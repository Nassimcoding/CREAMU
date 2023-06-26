using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FinalProject.ViewModel;
using System.IO;

namespace FinalProject.Controllers
{
    public class MembersController : Controller
    {
        private readonly CreamUdbContext _context;
        private readonly IWebHostEnvironment _images;

        public MembersController(CreamUdbContext context, IWebHostEnvironment images)
        {
            _context = context;
            _images = images;
        }

        // GET: Members
        public async Task<IActionResult> Index(int? page, key key)
        {
            ViewBag.StarYear = "--";
            ViewBag.EndYear = "--";
            string keyword = key.textkey;
            int pageSize = 10;
            int pageNumber = page ?? 1;

            // 從資料庫中獲取資料並包含相關的 Email 資料
            var data = await _context.Members.Include(e => e.Email).ToListAsync();

            int totalCount;
            int totalPages;
            IQueryable<Member> datas;

            if (string.IsNullOrEmpty(keyword))
            {
                if (key.StarYear == 0 && key.EndYear == 0)
                {
                    datas = _context.Members;

                    var pagedData = await datas.OrderBy(e => e.MemberId)
                                               .Skip((pageNumber - 1) * pageSize)
                                               .Take(pageSize)
                                               .ToListAsync();

                    totalCount = await datas.CountAsync();
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                    ViewBag.TotalPages = totalPages;
                    ViewBag.CurrentPage = pageNumber;

                    return View(pagedData);
                }
                else
                {
                    datas = _context.Members.Where(p => (p.JoinDate.Value.Year >= key.StarYear && p.JoinDate.Value.Year <= key.EndYear) ||
                                                         (p.Birthday.Value.Year >= key.StarYear && p.Birthday.Value.Year <= key.EndYear));

                    var pagedData = await datas.OrderBy(e => e.MemberId)
                                               .Skip((pageNumber - 1) * pageSize)
                                               .Take(pageSize)
                                               .ToListAsync();

                    totalCount = await datas.CountAsync();
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                    ViewBag.TotalPages = totalPages;
                    ViewBag.CurrentPage = pageNumber;
                    ViewBag.StarYear = key.StarYear;
                    ViewBag.EndYear = key.EndYear;

                    return View(pagedData);
                }
            }
            else
            {
                if (key.StarYear == 0 && key.EndYear == 0)
                {
                    datas = _context.Members.Where(p => p.Name.Contains(keyword) ||
                                                         p.Telephone.Contains(keyword) ||
                                                         p.Email.Email.Contains(keyword) ||
                                                         p.Address.Contains(keyword)
                                                        );

                    var pagedData = await datas.OrderBy(e => e.MemberId)
                                               .Skip((pageNumber - 1) * pageSize)
                                               .Take(pageSize)
                                               .ToListAsync();

                    totalCount = await datas.CountAsync();
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                    ViewBag.TotalPages = totalPages;
                    ViewBag.CurrentPage = pageNumber;

                    return View(pagedData);
                }
                else
                {
                    datas = _context.Members.Where(p => (p.Name.Contains(keyword) && p.JoinDate.Value.Year >= key.StarYear && p.JoinDate.Value.Year <= key.EndYear) ||
                                                         (p.Birthday.Value.Year >= key.StarYear && p.Birthday.Value.Year <= key.EndYear));

                    var pagedData = await datas.OrderBy(e => e.MemberId)
                                               .Skip((pageNumber - 1) * pageSize)
                                               .Take(pageSize)
                                               .ToListAsync();

                    totalCount = await datas.CountAsync();
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                    ViewBag.TotalPages = totalPages;
                    ViewBag.CurrentPage = pageNumber;
                    ViewBag.StarYear = key.StarYear;
                    ViewBag.EndYear = key.EndYear;

                    return View(pagedData);
                }
            }
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Email)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
 

      
       

        private string FileName(string path, IFormFile photo)
        {
            if (photo == null || string.IsNullOrEmpty(photo.ContentType) || string.IsNullOrEmpty(photo.FileName))
                return null;

            var extension = Path.GetExtension(photo.FileName); // 取得副檔名
            string[] allowExts = { ".jpg", ".jpeg", ".png", ".gif", ".tif", ".bmp" };
            if (!allowExts.Contains(extension))
            {
                return null;
            }

            // 產生新的檔案名稱，使用 Guid 來確保唯一性，並加上原始檔案的副檔名
            var newFileName = Guid.NewGuid().ToString("N") + extension;

            // 組合完整的檔案路徑
            var fullName = Path.Combine(path, newFileName);

            // 使用 FileStream 創建檔案流，以創建模式開啟或創建檔案
            using (var fileStream = new FileStream(fullName, FileMode.Create))
            {
                // 將照片的內容複製到檔案流中，進行儲存
                photo.CopyTo(fileStream);
            }

            // 返回新的檔案名稱
            return newFileName;
        }

        private async Task<bool> IsAccountsDuplicateAsync(string accounts)
        {
            // 可以使用資料庫查詢或其他方式來檢查重複性
            // 如果帳號已存在，則回傳 true，否則回傳 false

            // 示例：假設使用 Entity Framework Core
            var existingMembers =  await _context.Accounts.FirstOrDefaultAsync(e => e.Email == accounts);
            return existingMembers != null;
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.ToListAsync();
            ViewData["EmailId"] = new SelectList(_context.Accounts, "EmailId", "EmailId", member.EmailId);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Member member, IFormFile photo)
        {
            var accounts = await _context.Accounts.OrderBy(e => e.EmailId).ToListAsync();
            var members = await _context.Members.ToListAsync();
            // 查詢指定 id 的員工資料，並包含相關的 Email 資料
            var existingMember = await _context.Members
                                    .Include(e => e.Email) // 找到員工內的 EmailID 對應帳號 EmailID 的資料
                                    .FirstOrDefaultAsync(e => e.MemberId == id); // 第一個找到的值
            

            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (photo == null || photo.Length == 0)
            {
                ModelState.Remove("photo");
            }


            if (ModelState.IsValid)
            {
                try
                {
                    

                    string path = _images.WebRootPath + "/imgs/";


                    // 呼叫 NewEdit 方法以編輯現有的員工資料
                    NewEdit(member, photo, existingMember, path);

                    if (photo != null && existingMember.Image == null)
                    {
                        ModelState.AddModelError("Image", "請上傳正確照片格式 !");
                        return View(member);
                    }

                    _context.Update(existingMember);

                    // 更新 Account
                    existingMember.Email.Email = member.Email.Email;
                    _context.Update(existingMember.Email);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            
            ViewData["EmailId"] = new SelectList(_context.Accounts, "EmailId", "EmailId", member.EmailId);
            return View(member);
        }

        private void NewEdit(Member member, IFormFile photo, Member? existingMember, string path)
        {
            var filename = FileName(path, photo);
            existingMember.Image = (photo == null && existingMember.Image != null)? existingMember.Image : filename;
            existingMember.Name = member.Name;
            existingMember.Telephone = member.Telephone;
            existingMember.Password = member.Password;
            existingMember.Address = member.Address;
            existingMember.Birthday = member.Birthday;
            existingMember.JoinDate = member.JoinDate;
            existingMember.Notes = member.Notes;
            existingMember.Level = member.Level;
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Email)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Members == null)
            {
                return Problem("Entity set 'CreamUdbContext.Members'  is null.");
            }
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}
