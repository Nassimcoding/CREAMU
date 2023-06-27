using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;

namespace FinalProject.ViewModel
{
    public class EditOrdersController : Controller
    {
        private readonly CreamUdbContext _context;

        public EditOrdersController(CreamUdbContext context)
        {
            _context = context;
        }

        // GET: EditOrders
        public async Task<IActionResult> Index()
        {
            var creamUdbContext = _context.Orders
                .Include(o => o.Employee)
                .Include(o => o.Member)
                .Include(o => o.ShippingAddress);
            return View(await creamUdbContext.ToListAsync());
        }

        // GET: EditOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Employee)
                .Include(o => o.Member)
                .Include(o => o.ShippingAddress)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: EditOrders/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Address");
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Address");
            ViewData["ShippingAddressId"] = new SelectList(_context.PostAddresses, "AddressId", "AddressId");
            return View();
        }

        // POST: EditOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,EmployeeId,MemberId,OrderDate,TotalAmount,OrderStatus,PaymentStatus,ShippingAddressId,OrderNotes")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Address", order.EmployeeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Address", order.MemberId);
            ViewData["ShippingAddressId"] = new SelectList(_context.PostAddresses, "AddressId", "AddressId", order.ShippingAddressId);
            return View(order);
        }

        // GET: EditOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            //SelectList(dbcontext,reference table column , select target column,from table)
            //new SelectList(_context.Employees, "EmployeeId", "EmployeeId", order.EmployeeId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", order.EmployeeId);
            //var a = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", order.EmployeeId);
            
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", order.MemberId);
            ViewData["ShippingAddressId"] = new SelectList(_context.PostAddresses, "AddressId", "AddressId", order.ShippingAddressId);

            ViewData["VD_OrderID"] = id;
            return View(order);
        }

        // POST: EditOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,EmployeeId,MemberId,OrderDate,TotalAmount,OrderStatus,PaymentStatus,ShippingAddressId,OrderNotes")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Address", order.EmployeeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Address", order.MemberId);
            ViewData["ShippingAddressId"] = new SelectList(_context.PostAddresses, "AddressId", "AddressId", order.ShippingAddressId);
            return View(order);
        }

        // GET: EditOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Employee)
                .Include(o => o.Member)
                .Include(o => o.ShippingAddress)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: EditOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'CreamUdbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
