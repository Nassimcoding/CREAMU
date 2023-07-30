using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Data.DTO;



namespace FinalProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentsController : ControllerBase
    {
        private readonly CreamUdbContext _context;

        public ComponentsController(CreamUdbContext context)
        {
            _context = context;
        }

        // GET: api/Components
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComponentDTO>>> GetComponents()
        {
          if (_context.Components == null)
          {
              return NotFound();
          }
            var componentDTO = await _context.Components.Select(c => new ComponentDTO
            {
                ComponentId = c.ComponentId,
                ModelId = c.ModelId,
                MaterialId = c.MaterialId,
                ModelType = c.ModelType
            }).ToListAsync();
            return componentDTO;
            //return await _context.Components.ToListAsync();
        }

        [HttpGet]
        [Route("model/{modelId}/material/{materialId}")]
        public async Task<ActionResult<IEnumerable<ComponentDTO>>> GetComponents(int modelId, int materialId)
        {
            // 假設 components 的 modelId 和 materialId 分別對應至 model 和 material 的 id 屬性
                
            var componentDTO = await _context.Components
            .Where(c => c.ModelId == modelId && c.MaterialId == materialId)
            .Select(c => new ComponentDTO
            {
                ComponentId = c.ComponentId,
                MaterialId = c.MaterialId,
                ModelId = c.ModelId,
                ModelType = c.ModelType
            }).ToListAsync();

            return componentDTO;
        }
        
        // GET: api/Components/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Component>> GetComponent(int id)
        {
          if (_context.Components == null)
          {
              return NotFound();
          }
            var component = await _context.Components.FindAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            return component;
        }

        // PUT: api/Components/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponent(int id, Component component)
        {
            if (id != component.ComponentId)
            {
                return BadRequest();
            }

            _context.Entry(component).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentExists(id))
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

        // POST: api/Components
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Component>> PostComponent(Component component)
        {
          if (_context.Components == null)
          {
              return Problem("Entity set 'CreamUdbContext.Components'  is null.");
          }
            _context.Components.Add(component);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponent", new { id = component.ComponentId }, component);
        }

        // DELETE: api/Components/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponent(int id)
        {
            if (_context.Components == null)
            {
                return NotFound();
            }
            var component = await _context.Components.FindAsync(id);
            if (component == null)
            {
                return NotFound();
            }

            _context.Components.Remove(component);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComponentExists(int id)
        {
            return (_context.Components?.Any(e => e.ComponentId == id)).GetValueOrDefault();
        }
    }
}
