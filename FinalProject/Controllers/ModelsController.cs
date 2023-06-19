using Microsoft.AspNetCore.Mvc;
using FinalProject.Data;

namespace prjcreamUCustomized.Controllers
{

    public class ModelsController : Controller
    {
        private readonly CreamUdbContext econtext;
        public ModelsController(CreamUdbContext db)//建構子
        {
            econtext = db;
        }
        public IActionResult ModelList()
        {
            //table data to list
            var table = econtext.Models.ToList();
            return View(table);
        }
    }
}
