using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.ViewModel;

namespace FinalProject.Controllers
{
    public class MessagesController : Controller
    {
        private readonly CreamUdbContext _context;

        public MessagesController(CreamUdbContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> MessageList(CMessageKeywordViewModel vm)
        {
            string keyword = vm.txtKeyword;//輸入的關鍵字
            var creamUdbContext = _context.Messages.Include(m => m.Employee).Include(m => m.Member);//透過此方法可以同時附加Empolyee和Member的資料，以便於查詢
            IEnumerable<Message> datas = null;

            if (string.IsNullOrEmpty(keyword))//如果是null值
            {
                //就把所有資料都顯示出來
                datas = from m in creamUdbContext
                        select m;
            }
            else
                datas = creamUdbContext.Where(m => m.MessageContext.Contains(keyword) || (m.ReplyContext != null && m.ReplyContext.Contains(keyword)));//當m.ReplayContext不為null才會執行Contains判斷，可以用於確保m.Replay的例外狀況
            return View(datas);
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Employee)
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult AddNewMessage()
        {
            ViewData["MessageId"] = _context.Messages.OrderByDescending(d => d.MessageId).Select(d => d.MessageId).FirstOrDefault() + 1;
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name").Select(e => new SelectListItem
            {
                Value = e.Value.ToString(),
                Text = $"{e.Value} - {e.Text}"
            });
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name").Select(m => new SelectListItem
            {
                Value = m.Value.ToString(),
                Text = $"{m.Value} - {m.Text}"
            });
            return View();
        }   
  
        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
          [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewMessage([Bind("MessageId,MemberId,EmployeeId,Score,MessageTime,ReplyTime,MessageContext,ReplyContext,IsReply,IsShow,Image")] Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MessageList));
            }
            if (message.MessageContext == null)
            {
                message.MessageContext = string.Empty;
            }
            if (message.ReplyContext == null)
            {
                message.ReplyContext = string.Empty;
            }
            ViewData["MessageId"] = _context.Messages.OrderByDescending(d => d.MessageId).Select(d => d.MessageId).FirstOrDefault() + 1;
            
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", message.EmployeeId);
            //(資料集合, select選項的value, select選項的text, 預選值)
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", message.MemberId);//member的所有集合
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            //顯示員工與會員名單
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", message.EmployeeId).Select(e => new SelectListItem
            {
                Value = e.Value.ToString(),
                Text = $"{e.Value} - {e.Text}"
            });

            var members = _context.Members.ToList();
            var selectedMember = members.FirstOrDefault(m => m.MemberId == message.MemberId);

            if (selectedMember != null)
            {
                string memberIdAndName = $"{selectedMember.MemberId} - {selectedMember.Name}";
                ViewData["MemberId"] = memberIdAndName;
            }
            else
            {
                ViewData["MemberId"] = string.Empty;
            }
            /*ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name", message.MemberId).Select(e => new SelectListItem
            {
                Value = e.Value.ToString(),
                Text = $"{e.Value} - {e.Text}"
            });*/
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageId,MemberId,EmployeeId,Score,MessageTime,ReplyTime,MessageContext,ReplyContext,IsReply,IsShow,Image")] Message message)
        {
            if (id != message.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.MessageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MessageList));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", message.EmployeeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", message.MemberId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Employee)
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Messages == null)
            {
                return Problem("Entity set 'CreamUdbContext.Messages'  is null.");
            }
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MessageList));
        }

        private bool MessageExists(int id)
        {
          return (_context.Messages?.Any(e => e.MessageId == id)).GetValueOrDefault();
        }
    }
}
