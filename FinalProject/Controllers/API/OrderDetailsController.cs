using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Data.DTO;
using Microsoft.CodeAnalysis;

namespace FinalProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly CreamUdbContext _context;

        public OrderDetailsController(CreamUdbContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public IEnumerable<OrderDetailsDTO> GetOrderDetail(int id)
        {
            return _context.OrderDetails.Where(o => o.OrderId == id).Select(p => new OrderDetailsDTO
            {
                OrderDetailId = p.OrderDetailId,
                Discount = p.Discount,
                ProductId = p.ProductId,
                Qty = p.Qty,
                Subtotal = p.Subtotal,
                UnitPrice = p.UnitPrice,
                Type = p.Type,
                Notes = p.Notes
            });
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return "修改失敗!";
            }

            try
            {
                await putdata(id, orderDetail);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
                {
                    return "修改失敗!";
                }
            }

            return "修改成功!";
        }

        private async Task putdata(int id, OrderDetail orderDetail)
        {
            var checkOrderDetail = await _context.OrderDetails.FindAsync(id);
            checkOrderDetail.OrderDetailId = orderDetail.OrderDetailId;
            checkOrderDetail.Discount = orderDetail.Discount;
            checkOrderDetail.ProductId = orderDetail.ProductId;
            checkOrderDetail.Qty = orderDetail.Qty;
            checkOrderDetail.Subtotal = orderDetail.Subtotal;
            checkOrderDetail.UnitPrice = orderDetail.UnitPrice;
            checkOrderDetail.Type = orderDetail.Type;
            checkOrderDetail.Notes = orderDetail.Notes;

            _context.Entry(checkOrderDetail).State = EntityState.Modified;
        }

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
          if (_context.OrderDetails == null)
          {
              return Problem("Entity set 'CreamUdbContext.OrderDetails'  is null.");
          }
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderDetail", new { id = orderDetail.OrderDetailId }, orderDetail);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetails?.Any(e => e.OrderDetailId == id)).GetValueOrDefault();
        }
    }
}
