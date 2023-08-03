using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;

namespace FinalProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderimgsController : ControllerBase
    {
        private readonly CreamUdbContext _context;
        private readonly IWebHostEnvironment _images;

        public OrderimgsController(CreamUdbContext context, IWebHostEnvironment images)
        {
            _context = context;
            _images = images;
        }

        public class PhotoImage { public IFormFile photo { get; set; } }

        // PUT: api/Orderimgs/${oriderId}/upload
        [HttpPost("{oriderId}/upload")]
        public async Task<string> ChangeFile(int orderId, [FromForm] PhotoImage photo)
        {
            string path = _images.WebRootPath + "/orderImgs/";
            Directory.CreateDirectory(path);

            var orider = await _context.OrderDetails.FindAsync(orderId);
            if (photo == null)
                return "上傳失敗!";

            var newFileName = Guid.NewGuid().ToString("N") + ".jpg";
            var newFileURL = Path.Combine(path, newFileName);

            // 儲存新的照片
            using (var fileStream = new FileStream(newFileURL, FileMode.Create))
            {
                await photo.photo.CopyToAsync(fileStream);
            }
            //  await _context.SaveChangesAsync();
            return newFileName;
        }
        //GET:api/Orderimgs
        [HttpGet]
        [Route("/api/saveOrderimgs")]
        public async Task<string>saveOrderimgs(string? orderimgs)
        {
            if(orderimgs == null || orderimgs.Length == 0)
            {
                return "Nodata received拉";
            }
            try
            {
                return "ok拉";

            }catch (Exception ex)
            {
                return "fail拉";
            }
        }

        // GET: api/Orderimgs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orderimg>>> GetOrderimgs()
        {
          if (_context.Orderimgs == null)
          {
              return NotFound();
          }
            return await _context.Orderimgs.ToListAsync();
        }

        // GET: api/Orderimgs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orderimg>> GetOrderimg(int id)
        {
          if (_context.Orderimgs == null)
          {
              return NotFound();
          }
            var orderimg = await _context.Orderimgs.FindAsync(id);

            if (orderimg == null)
            {
                return NotFound();
            }

            return orderimg;
        }

        // PUT: api/Orderimgs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderimg(int id, Orderimg orderimg)
        {
            if (id != orderimg.ImgId)
            {
                return BadRequest();
            }

            _context.Entry(orderimg).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderimgExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orderimgs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orderimg>> PostOrderimg(Orderimg orderimg)
        {
          if (_context.Orderimgs == null)
          {
              return Problem("Entity set 'CreamUdbContext.Orderimgs'  is null.");
          }
            _context.Orderimgs.Add(orderimg);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderimg", new { id = orderimg.ImgId }, orderimg);
        }

        // DELETE: api/Orderimgs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderimg(int id)
        {
            if (_context.Orderimgs == null)
            {
                return NotFound();
            }
            var orderimg = await _context.Orderimgs.FindAsync(id);
            if (orderimg == null)
            {
                return NotFound();
            }

            _context.Orderimgs.Remove(orderimg);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderimgExists(int id)
        {
            return (_context.Orderimgs?.Any(e => e.ImgId == id)).GetValueOrDefault();
        }
    }
}
