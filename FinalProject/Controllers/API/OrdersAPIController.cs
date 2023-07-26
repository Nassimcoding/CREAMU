using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Data.DTO;
using static NuGet.Packaging.PackagingConstants;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace FinalProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersAPIController : ControllerBase
    {
        private readonly CreamUdbContext _context;

        public OrdersAPIController(CreamUdbContext context)
        {
            _context = context;
        }

        //// GET: api/OrdersAPI/5
        [HttpGet("{id}")]
        public IEnumerable<OrderDTO> GetOrder(int id)
        {
            var orders =  _context.Orders
                             .Include(m => m.Member).Where(m => m.MemberId == id).Select(p => new OrderDTO
                             {
                                 OrderId = p.OrderId,
                                 OrderDate = p.OrderDate,
                                 TotalAmount = p.TotalAmount,
                                 OrderStatus = p.OrderStatus,
                                 PaymentStatus = p.PaymentStatus,
                                
                             });
            return orders;
        }

        // DELETE: api/OrdersAPI/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteOrder(int id)
        {
            if (_context.Orders == null) return "刪除失敗!!";

            var order = await _context.Orders.FindAsync(id);

            if (order == null) return "刪除失敗!!";

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return "刪除成功!!";
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
