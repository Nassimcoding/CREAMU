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
    public class OrderDetailsAPIController : ControllerBase
    {
        private readonly CreamUdbContext _context;

        public OrderDetailsAPIController(CreamUdbContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetailsAPI/5
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

        // PUT: api/OrderDetailsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutOrderDetail(int id, OrderDetailsDTO orderDetailsDTO)
        {
            if (id != orderDetailsDTO.OrderDetailId) return "修改失敗!";
            
            try
            {
                await putdata(id, orderDetailsDTO);
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

        private async Task putdata(int id, OrderDetailsDTO orderDetailsDTO)
        {
            var checkOrderDetail = await _context.OrderDetails.FindAsync(id);
            checkOrderDetail.OrderDetailId = orderDetailsDTO.OrderDetailId;
            checkOrderDetail.Discount = orderDetailsDTO.Discount;
            checkOrderDetail.ProductId = orderDetailsDTO.ProductId;
            checkOrderDetail.Qty = orderDetailsDTO.Qty;
            checkOrderDetail.Subtotal = orderDetailsDTO.Subtotal;
            checkOrderDetail.UnitPrice = orderDetailsDTO.UnitPrice;
            checkOrderDetail.Type = orderDetailsDTO.Type;
            checkOrderDetail.Notes = orderDetailsDTO.Notes;

            _context.Entry(checkOrderDetail).State = EntityState.Modified;
        }

        //-----------------------   這個只是測試刪除能不能使用   -------------------------------------
        // POST: api/OrderDetailsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost] 
        //public async Task<string> PostOrderDetail(OrderDetailsDTO orderDetailsDTO)
        //{
        //    if (_context.OrderDetails.Find(orderDetailsDTO.OrderDetailId) != null) return "新增失敗!";
        //    OrderDetail createOrderDetails = CreateorderDetail(orderDetailsDTO);
        //    _context.OrderDetails.Add(createOrderDetails);
        //    await _context.SaveChangesAsync();

        //    return "新增成功!";
        //}

        //private static OrderDetail CreateorderDetail(OrderDetailsDTO orderDetailsDTO)
        //{
        //    return new OrderDetail
        //    {
        //        Discount = orderDetailsDTO.Discount,
        //        Notes = orderDetailsDTO.Notes,
        //        ProductId = orderDetailsDTO.ProductId,
        //        Qty = orderDetailsDTO.Qty,
        //        Subtotal = orderDetailsDTO.Subtotal,
        //        Type = orderDetailsDTO.Type,
        //        UnitPrice = orderDetailsDTO.UnitPrice,
        //    };
        //}

        // DELETE: api/OrderDetailsAPI/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null) return "刪除失敗!";

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return "刪除成功!";
        }
        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetails?.Any(e => e.OrderDetailId == id)).GetValueOrDefault();
        }
    }
}
