using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Data.DTO;
using System.Text.Json;

namespace FinalProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombineDetailsController : ControllerBase
    {
        private readonly CreamUdbContext _context;

        public CombineDetailsController(CreamUdbContext context)
        {
            _context = context;
        }
        
        // GET: api/CombineDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CombineDetail>>> GetCombineDetails()
        {
          if (_context.CombineDetails == null)
          {
              return NotFound();
          }
            return await _context.CombineDetails.ToListAsync();
        }

        // GET: api/CombineDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CombineDetail>> GetCombineDetail(int id)
        {
          if (_context.CombineDetails == null)
          {
              return NotFound();
          }
            var combineDetail = await _context.CombineDetails.FindAsync(id);

            if (combineDetail == null)
            {
                return NotFound();
            }

            return combineDetail;
        }

        // PUT: api/CombineDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCombineDetail(int id, CombineDetail combineDetail)
        {
            if (id != combineDetail.CombineId)
            {
                return BadRequest();
            }

            _context.Entry(combineDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CombineDetailExists(id))
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

        // POST: api/CombineDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       /* [HttpPost]
        public async Task<ActionResult<CombineDetail>> PostCombineDetail(CombineDetail combineDetail)
        {
            if (_context.CombineDetails == null)
            {
                return Problem("Entity set 'CreamUdbContext.CombineDetails'  is null.");
            }
            _context.CombineDetails.Add(combineDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCombineDetail", new { id = combineDetail.CombineId }, combineDetail);
        }*/

        // POST: api/CombineDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet]
        [Route("/api/saveCombineDetail")]
        public async Task<string> saveCombineDetail(string? combineDetail)
        {
            if (combineDetail == null || combineDetail.Length == 0)
            {
                return "No data received.";
            }
            try
            {
                List<CombineDetailDTO> CDs = JsonSerializer.Deserialize<List<CombineDetailDTO>>(combineDetail);
                //CombineDetailDTO CDs = JsonSerializer.Deserialize<CombineDetailDTO>(combineDetail);

                foreach(var item in CDs)
                {
                    CombineDetail CD = new CombineDetail();
                    CD.Chead = Convert.ToInt32(item.Chead);
                    CD.Cbody = Convert.ToInt32(item.Cbody);
                    CD.Crhand = Convert.ToInt32(item.Crhand);
                    CD.Clhand = Convert.ToInt32(item.Clhand);
                    CD.Crfoot = Convert.ToInt32(item.Crfoot);
                    CD.Clfoot = Convert.ToInt32(item.Clfoot);
                    CD.SubTotal = Convert.ToInt32(item.SubTotal);
                    CD.Type = Convert.ToString(item.Type);
                    Console.WriteLine(CD);
                    _context.CombineDetails.Add(CD);
                }
                    _context.SaveChanges();

                return "ok";
            }
            catch(Exception ex)
            {
                return "fail";
            }
        }

        // DELETE: api/CombineDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCombineDetail(int id)
        {
            if (_context.CombineDetails == null)
            {
                return NotFound();
            }
            var combineDetail = await _context.CombineDetails.FindAsync(id);
            if (combineDetail == null)
            {
                return NotFound();
            }

            _context.CombineDetails.Remove(combineDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CombineDetailExists(int id)
        {
            return (_context.CombineDetails?.Any(e => e.CombineId == id)).GetValueOrDefault();
        }
    }
}
