using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Data.DTO;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempOrderDetailsAPIController : ControllerBase
    {
        private readonly CreamUdbContext _context;

        public TempOrderDetailsAPIController(CreamUdbContext context)
        {
            _context = context;
        }

        // GET: api/TempOrderDetailsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDisplayDetailDTO>>> GetTempOrderDetails()
        {
            if (_context.TempOrderDetails == null)
            {
                return NotFound();
            }
            else
            {
                var context_tod = _context.TempOrderDetails;
                var context_mt = _context.Members;
                var a = context_mt.Where(e => e.MemberId == context_tod.FirstOrDefault(y1 => y1.MemberId == 1).MemberId).Select(e => e.Name);
                var context_prod = _context.Products;
                var collectCDD_DTO = await context_tod.Select(tod => new CartDisplayDetailDTO
                {
                    id = tod.OrderDetailId,
                    mId = tod.MemberId,
                    //ok
                    mName = context_mt.FirstOrDefault(e => e.MemberId == tod.MemberId).Name,

                    pId = tod.ProductId,
                    //ok
                    pName = context_prod.FirstOrDefault(e => e.ProductId == tod.ProductId).ProductName,
                    //ok
                    descript = context_prod.FirstOrDefault(e => e.ProductId == tod.ProductId).Descript,
                    qty = tod.Qty,
                    unitPrice = tod.UnitPrice,
                    discount = tod.Discount,
                    subtotal = tod.Subtotal,
                    notes = tod.Notes,
                    type = tod.Type,
                }).ToListAsync();
                return collectCDD_DTO;
            }
        }

        //get member temporderdetail data
        // GET: api/TempOrderDetailsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CartDisplayDetailDTO>>> GetTempOrderDetail(int id)
        {
            if (_context.TempOrderDetails == null)
            {
                return NotFound();
            }
            else
            {
                var context_tod = _context.TempOrderDetails.Where(e => e.MemberId == id);
                var context_mt = _context.Members;
                var a = context_mt.Where(e => e.MemberId == context_tod.FirstOrDefault(y1 => y1.MemberId == 1).MemberId).Select(e => e.Name);
                var context_prod = _context.Products;
                var collectCDD_DTO = await context_tod.Select(tod => new CartDisplayDetailDTO
                {
                    id = tod.OrderDetailId,
                    mId = tod.MemberId,
                    //ok
                    mName = context_mt.FirstOrDefault(e => e.MemberId == tod.MemberId).Name,

                    pId = tod.ProductId,
                    //ok
                    pName = context_prod.FirstOrDefault(e => e.ProductId == tod.ProductId).ProductName,
                    //ok
                    descript = context_prod.FirstOrDefault(e => e.ProductId == tod.ProductId).Descript,
                    qty = tod.Qty,
                    unitPrice = tod.UnitPrice,
                    discount = tod.Discount,
                    subtotal = tod.Subtotal,
                    notes = tod.Notes,
                    type = tod.Type,
                }).ToListAsync();
                return collectCDD_DTO;
            }
        }

        // PUT: api/TempOrderDetailsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTempOrderDetail(int id, TempOrderDetail tempOrderDetail)
        {
            if (id != tempOrderDetail.OrderDetailId)
            {
                return BadRequest();
            }

            _context.Entry(tempOrderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempOrderDetailExists(id))
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


        // comfirm purchase 
        // POST: api/TempOrderDetailsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostTempOrderOrder([FromBody] orderSentModel orderdata)
        {
            if (_context.TempOrderDetails.FirstOrDefaultAsync(e => e.MemberId == 1) == null)
            {
                return "None Data";
            }
            var context_order = _context.Orders;
            var context_tod = _context.TempOrderDetails;
            //var temporder = context_order.Max(e => e.OrderId) + 1;
            //return Convert.ToString(orderdata.totalp);
            await _context.Orders.AddAsync(new Order
            {
                EmployeeId = orderdata.id,
                //MemberId = id,
                MemberId = 1,
                OrderDate = DateTime.Now,
                //TotalAmount = context_tod.Sum(e => e.Subtotal),
                TotalAmount = orderdata.totalp,
                OrderStatus = "0",
                PaymentStatus = "0",
                ShippingAddressId = null,
                OrderNotes = null,
            });
            // have bug if you click too fast will be ignore for whlle
            System.Threading.Thread.Sleep(500);
            await _context.SaveChangesAsync();
            System.Threading.Thread.Sleep(500);
            if (_context.TempOrderDetails.FirstOrDefault() == null)
            {
                return "sent purchase order success";
            }
            //find order max id
            int OrderMaxId;
            if (_context.Orders.FirstOrDefault() == null)
            {
                OrderMaxId = 1;
            }
            else
            {
                OrderMaxId = _context.Orders.Max(e => e.OrderId);
            }

            //transfer temporderdetail to orderdetail
            var tempremove = await _context.TempOrderDetails.ToListAsync();
            //use transfer variable to transfer temporderdetail to prderdetail
            var temporderdetail = new List<OrderDetail>();
            for (int i = 0; i < tempremove.Count; i++)
            {
                //orderdetail id will auto add 1
                temporderdetail.Add(new OrderDetail
                {

                    OrderId = OrderMaxId,
                    ProductId = tempremove[i].ProductId,
                    Qty = tempremove[i].Qty,
                    UnitPrice = tempremove[i].UnitPrice,
                    Discount = tempremove[i].Discount,
                    Subtotal = tempremove[i].Subtotal,
                    Notes = tempremove[i].Notes,
                    Type = tempremove[i].Type,
                });
            }
            _context.OrderDetails.AddRange(temporderdetail);

            //remove all tenporderdetail data
            _context.TempOrderDetails.RemoveRange(tempremove);
            await _context.SaveChangesAsync();
            return "sent purchase order success";
        }

        // DELETE: api/TempOrderDetailsAPI/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteTempOrderDetail(int id)
        {
            if (_context.TempOrderDetails == null)
            {
                return "Not find data";
            }
            var tempOrderDetail = await _context.TempOrderDetails.FindAsync(id);
            if (tempOrderDetail == null)
            {
                return "Not find data";
            }

            _context.TempOrderDetails.Remove(tempOrderDetail);
            await _context.SaveChangesAsync();

            return "Delete Data Success";
        }

        private bool TempOrderDetailExists(int id)
        {
            return (_context.TempOrderDetails?.Any(e => e.OrderDetailId == id)).GetValueOrDefault();
        }
    }
}
