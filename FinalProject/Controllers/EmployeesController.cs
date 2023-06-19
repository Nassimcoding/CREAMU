using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;
//using AspNetCore;

namespace FinalProject.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CreamUdbContext _context;
        private readonly IWebHostEnvironment _images;

        public EmployeesController(CreamUdbContext context, IWebHostEnvironment images)
        {
            _context = context;
            _images = images;
        }

        // GET: Employees
        public async Task<IActionResult> Index(int? page, key key)
        {
            // 取得搜尋關鍵字
            string keyword = key.textkey;
            int pageSize = 6; // 每頁顯示的資料數量
            int pageNumber = page ?? 1; // 當前頁數，如果為空則默認為第1頁

            // 從資料庫中獲取資料並包含相關的 Email 資料
            var data = await _context.Employees.Include(e => e.Email).ToListAsync();

            var totalCount = data.Count(); // 總資料數量
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 總頁數

            IEnumerable<Employee> datas = null;
            if (string.IsNullOrEmpty(keyword))
            {
                // 如果沒有搜尋關鍵字，則返回所有的資料
                datas = from p in _context.Employees
                        select p;

                // 取得當前頁的資料，並按照 EmployeeId 升序排序
                var pagedData = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(e => e.EmployeeId).ToList();

                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber;

                return View(pagedData);
            }
            else
            {
                // 如果有搜尋關鍵字，則根據關鍵字進行搜尋
                datas = _context.Employees.Where(p => p.Name.Contains(keyword) ||
                                                     p.Telephone.Contains(keyword) ||
                                                     p.Email.Email.Contains(keyword) ||
                                                     p.Address.Contains(keyword) ||
                                                     p.Title.Contains(keyword));


                return View(datas);
            }
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Email)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["EmailId"] = new SelectList(_context.Accounts, "EmailId", "EmailId");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeesViewModel EmployeesViewModel, IFormFile photo)
        {
            Employee Newcreate = new Employee();
            Account account = new Account();
            if (ModelState.IsValid)
            {
                
                if (IsAccountsDuplicate(EmployeesViewModel.Email))
                {
                    ModelState.AddModelError("Email", "帳號已存在。");
                    return View(EmployeesViewModel);
                }

                string path = _images.WebRootPath + "/imgs/" + Guid.NewGuid().ToString() + ".jpg";

                NewCreate(EmployeesViewModel, Newcreate, account, path, photo);
                // 取得最新創建的帳號
                var newCreate = _context.Accounts
                    .OrderByDescending(e => e.EmailId)  // 以 CreatedDate 欄位降序排序
                    .FirstOrDefault();

                // 將新帳號的 EmailId 賦值給創建的員工
                Newcreate.EmailId = newCreate.EmailId;

                _context.Add(Newcreate);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmailId"] = new SelectList(_context.Accounts, "EmailId", "EmailId", EmployeesViewModel.EmailId);
            return View(EmployeesViewModel);
        }

        private bool IsAccountsDuplicate(object accounts)
        {
            // 在這裡進行檢查帳號是否已存在的邏輯
            // 可以使用資料庫查詢或其他方式來檢查重複性
            // 如果帳號已存在，則回傳 true，否則回傳 false

            // 示例：假設使用 Entity Framework Core
            var existingEmployee = _context.Accounts.FirstOrDefault(e => e.Email == accounts);
            return existingEmployee != null;
        }

        private void NewCreate(EmployeesViewModel employee, Employee Newcreate, Account account, string path, IFormFile photo)
            {

            Newcreate.JoinDate = DateTime.Now;
            Newcreate.Name = employee.Name;
            Newcreate.Address = employee.Address;
            Newcreate.Notes = employee.Notes;
            Newcreate.Telephone = employee.Telephone;
            Newcreate.Birthday = employee.Birthday;
            Newcreate.Title = employee.Title;
            Newcreate.Password = employee.Password;
            if (photo != null)
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
                Newcreate.Image = Path.GetFileName(path);
            }
            else
            {
                Newcreate.Image = null;
            }


            account.Email = employee.Email;
            _context.Add(account);

           



        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["EmailId"] = new SelectList(_context.Accounts, "EmailId", "EmailId", employee.EmailId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee, Account account, IFormFile photo)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // 查詢指定 id 的員工資料，並包含相關的 Email 資料
                    var existingEmployee = await _context.Employees
                                            .Include(e => e.Email) // 找到員工內的 EmailID 對應帳號 EmailID 的資料
                                            .FirstOrDefaultAsync(e => e.EmployeeId == id); // 第一個找到的值

                    if (existingEmployee == null)
                    {
                        return NotFound();
                    }


                    string path = _images.WebRootPath + "/imgs/" + Guid.NewGuid().ToString() + ".jpg";


                    // 呼叫 NewEdit 方法以編輯現有的員工資料
                    NewEdit(employee, existingEmployee, path, photo);

                    // 更新 Employee
                    _context.Update(existingEmployee);

                    // 更新 Account
                    existingEmployee.Email.Email = employee.Email.Email;
                    _context.Update(existingEmployee.Email);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            ViewData["EmailId"] = new SelectList(_context.Accounts, "EmailId", "EmailId", employee.EmailId);
            return View(employee);
        }

        // NewEdit 方法用於更新現有員工的屬性
        private static void NewEdit(Employee employee, Employee existingEmployee, string path, IFormFile photo)
        {


            existingEmployee.Name = employee.Name;
            existingEmployee.Telephone = employee.Telephone;
            existingEmployee.Password = employee.Password;
            existingEmployee.Address = employee.Address;
            existingEmployee.Birthday = employee.Birthday;
            existingEmployee.JoinDate = employee.JoinDate;
            existingEmployee.Notes = employee.Notes;
            
            if (photo != null )
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
                existingEmployee.Image = Path.GetFileName(path);
            }
            else
            {
                existingEmployee.Image = null;
            }
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Email)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'CreamUdbContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
