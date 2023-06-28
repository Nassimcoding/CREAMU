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
using Microsoft.CodeAnalysis.FlowAnalysis;
using NuGet.Packaging.Signing;
using System.Collections.Immutable;
using Microsoft.IdentityModel.Tokens;
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
            ViewBag.StarYear = "";
            ViewBag.EndYear = "";

            string keyword = key.textkey;
            int pageSize = 6;
            int pageNumber = page ?? 1;

            // 從資料庫中獲取資料並包含相關的 Email 資料
            var data = await _context.Employees.Include(e => e.Email).ToListAsync();

            int totalCount;
            int totalPages;
            IQueryable<Employee> datas;

            if (string.IsNullOrEmpty(keyword))
            {
                
                if (key.StarYear == 0 && key.EndYear == 0)
                {
                    datas = _context.Employees;

                    var pagedData = await datas.OrderBy(e => e.EmployeeId)
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
                    datas = _context.Employees.
                        Where(p => (p.JoinDate.Value.Year >= key.StarYear && p.JoinDate.Value.Year <= key.EndYear) ||
                             (p.Birthday.Value.Year >= key.StarYear && p.Birthday.Value.Year <= key.EndYear));

                    var pagedData = await datas.OrderBy(e => e.EmployeeId)
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
                    datas = _context.Employees.Where(p => p.Name.Contains(keyword) ||
                                                         p.Telephone.Contains(keyword) ||
                                                         p.Email.Email.Contains(keyword) ||
                                                         p.Address.Contains(keyword) ||
                                                         p.Title.Contains(keyword));

                    var pagedData = await datas.OrderBy(e => e.EmployeeId)
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
                    datas = _context.Employees.Where(p => (p.Name.Contains(keyword) && p.JoinDate.Value.Year >= key.StarYear && p.JoinDate.Value.Year <= key.EndYear) ||
                                                         (p.Birthday.Value.Year >= key.StarYear && p.Birthday.Value.Year <= key.EndYear) && (p.Name.Contains(keyword) ||
                                                         p.Telephone.Contains(keyword) ||
                                                         p.Email.Email.Contains(keyword) ||
                                                         p.Address.Contains(keyword) ||
                                                         p.Title.Contains(keyword)));

                    var pagedData = await datas.OrderBy(e => e.EmployeeId)
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

            if (photo == null || photo.Length == 0)
            {
                ModelState.Remove("photo");
            }

            if (ModelState.IsValid)
            {

                if (IsAccountsDuplicate(EmployeesViewModel.Email))
                {
                    ModelState.AddModelError("Email", "帳號已存在。");
                    return View(EmployeesViewModel);
                }
                string path = _images.WebRootPath + "/imgs/";

                NewCreate(EmployeesViewModel, Newcreate, account, path, photo);

                if (photo != null && Newcreate.Image == null)
                {
                    ModelState.AddModelError("Image", "請上傳正確照片格式 !");
                    return View(EmployeesViewModel);
                }
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

        // 在這裡進行檢查帳號是否已存在的邏輯
        private bool IsAccountsDuplicate(object accounts)
        {
           
            // 可以使用資料庫查詢或其他方式來檢查重複性
            // 如果帳號已存在，則回傳 true，否則回傳 false

            // 示例：假設使用 Entity Framework Core
            var existingEmployee = _context.Accounts.FirstOrDefault(e => e.Email == accounts);
            return existingEmployee != null;
        }

        // 新建資料的格式
        private void NewCreate(EmployeesViewModel employee, Employee Newcreate, Account account, string path, IFormFile photo)
        {
            var filename = FileName(path, photo);
            Newcreate.Image = filename;
            Newcreate.JoinDate = DateTime.Now;
            Newcreate.Name = employee.Name;
            Newcreate.Address = employee.Address;
            Newcreate.Notes = employee.Notes;
            Newcreate.Telephone = employee.Telephone;
            Newcreate.Birthday = employee.Birthday;
            Newcreate.Title = employee.Title;
            Newcreate.Password = employee.Password;
            
            account.Email = employee.Email; // 因須先有帳號才會建立成功
            _context.Add(account); // 先把帳號加入
        }

        // 儲存照片的方法
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
            var accounts = await _context.Accounts.OrderBy(e => e.EmailId).ToListAsync();
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

            if (photo == null || photo.Length == 0)
            {
                ModelState.Remove("photo");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (IsAccountsDuplicate(employee.Email.Email))
                    {
                        ModelState.AddModelError("Email.Email", "帳號已存在。");
                        return View(employee);
                    }
                    // 查詢指定 id 的員工資料，並包含相關的 Email 資料
                    var existingEmployee = await _context.Employees
                                            .Include(e => e.Email) // 找到員工內的 EmailID 對應帳號 EmailID 的資料
                                            .FirstOrDefaultAsync(e => e.EmployeeId == id); // 第一個找到的值

                    if (existingEmployee == null)
                    {
                        return NotFound();
                    }

                   

                    string path = _images.WebRootPath + "/imgs/";


                    // 呼叫 NewEdit 方法以編輯現有的員工資料
                    NewEdit(employee, existingEmployee, path, photo);

                    if (photo != null && existingEmployee.Image == null)
                    {
                        ModelState.AddModelError("Image", "請上傳正確照片格式 !");
                        return View(employee);
                    }
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
        private  void NewEdit(Employee employee, Employee existingEmployee, string path, IFormFile photo)
        {

            var filename = FileName(path, photo);
            existingEmployee.Image = (photo == null && existingEmployee.Image != null) ? existingEmployee.Image : filename;
            existingEmployee.Name = employee.Name;
            existingEmployee.Telephone = employee.Telephone;
            existingEmployee.Password = employee.Password;
            existingEmployee.Address = employee.Address;
            existingEmployee.Birthday = employee.Birthday;
            existingEmployee.JoinDate = employee.JoinDate;
            existingEmployee.Notes = employee.Notes;

           
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
