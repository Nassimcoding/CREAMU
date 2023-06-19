using Microsoft.AspNetCore.Mvc;
using FinalProject.Data;

namespace FinalProject.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly CreamUdbContext econtext;
        public ComponentsController(CreamUdbContext db)//建構子
        {
            econtext = db;
        }
        public IActionResult ComponentList()
        {
            //table data to list
            var table = econtext.Components.ToList();
            return View(table);
        }
        
    }
}
