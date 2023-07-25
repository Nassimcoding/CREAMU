﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Data.DTO;
using Microsoft.AspNetCore.Cors;

namespace FinalProject.Controllers.API
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class MembersAPIController : ControllerBase
    {
        private readonly CreamUdbContext _context;
        private readonly IWebHostEnvironment _images;

        public MembersAPIController(CreamUdbContext context, IWebHostEnvironment images)
        {
            _context = context;
            _images = images;
        }

        // GET: api/MembersAPI/5
        [HttpGet("{id}")]
        public async Task<MembersDTO>? GetMember(int id)
        {
            var data = await _context.Members.Include(e => e.Email).ToListAsync();
            var checkMember = _context.Members.Find(id);

            if (checkMember == null)  return null; 

            return ResultMember(checkMember);
        }

        private MembersDTO ResultMember(Member checkMember)
        {
            return new MembersDTO
            {
                MemberId = checkMember.MemberId,
                Name = checkMember.Name,
                Telephone = checkMember.Telephone,
                Email = checkMember.Email.Email,
                Address = checkMember.Address,
                Birthday = (DateTime)checkMember.Birthday,
                JoinDate = (DateTime)checkMember.JoinDate,
                Level = (int)checkMember.Level,
                Image = checkMember.Image,
                Notes = checkMember.Notes,
            };
        }

        // PUT: api/MembersAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutMember(int id, [FromBody] MembersDTO member)
        {
            if (id != member.MemberId) return "修改會員資料失敗!";

            var checkMember = await _context.Members.FindAsync(id);

            if (checkMember == null) return "修改會員資料失敗!";

            try
            {
                ReviseMember(member, checkMember);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
                    return "修改會員資料失敗!";
                else
                    throw;
            }
            return "修改員工記錄成功!";
        }

        private void ReviseMember(MembersDTO member, Member checkMember)
        {
            Account account = _context.Accounts.Find(checkMember.EmailId);
            checkMember.Name = member.Name;
            checkMember.Telephone = member.Telephone;
            account.Email = member.Email;
            checkMember.Address = member.Address;
            checkMember.Birthday = member.Birthday;
            checkMember.JoinDate = member.JoinDate;
            checkMember.Level = member.Level;
            checkMember.Image = member.Image;
            checkMember.Notes = member.Notes;
            _context.Entry(checkMember).State = EntityState.Modified;
        }

        // POST: api/MembersAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostMember(MembersCreateDTO member)
        {
            await CreateMember(member);

            return "新增成功!";
        }

        private async Task CreateMember(MembersCreateDTO member)
        {
            Account account = new Account { Email = member.Email };
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            var emailid = _context.Accounts.OrderByDescending(e => e.EmailId).First();

            Member create = new Member
            {
                Name = member.Name,
                Telephone = member.Telephone,
                EmailId = emailid.EmailId,
                Address = member.Address,
                Birthday = member.Birthday,
                JoinDate = DateTime.Now,
                Level = 1,
                Image = member.Image,
                Notes = member.Notes,
                Password = member.Password,
            };
            _context.Members.Add(create);
            await _context.SaveChangesAsync();
        }

        public class PhotoImage { public IFormFile photo { get; set; } }

        // PUT: api/MembersAPI/${id}/upload
        [HttpPost("{id}/upload")]
        public async Task<string> ChangeFile(int id, [FromForm] PhotoImage photo)
        {
            string path = _images.WebRootPath + "/imgs/";

            var member = await _context.Members.FindAsync(id);
            if (photo == null || string.IsNullOrEmpty(photo.photo.ContentType) || string.IsNullOrEmpty(photo.photo.FileName)) return "上傳失敗!";

            var extension = Path.GetExtension(photo.photo.FileName); // 取得副檔名
            string[] allowExts = { ".jpg", ".jpeg", ".png", ".gif", ".tif", ".bmp" };

            if (!allowExts.Contains(extension)) return "上傳格式錯誤!";

            // 刪除舊照片檔案
            if (!string.IsNullOrEmpty(member.Image))
            {
                var oldFilePath = Path.Combine(path, member.Image);
                if (System.IO.File.Exists(oldFilePath))
                    System.IO.File.Delete(oldFilePath);
            }

            
            var newFileName = Guid.NewGuid().ToString("N") + extension;
            var newFileURL = Path.Combine(path, newFileName);

            // 儲存新的照片
            using (var fileStream = new FileStream(newFileURL, FileMode.Create))
            {
                await photo.photo.CopyToAsync(fileStream);
            }

            // 更新會員照片
            member.Image = newFileName;

            await _context.SaveChangesAsync();
            return newFileName;
        }

        // DELETE: api/MembersAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            if (_context.Members == null)
            {
                return NotFound();
            }
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberExists(int id)
        {
            return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}