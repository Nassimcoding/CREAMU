using Microsoft.AspNetCore.Mvc;
using FinalProject.Data;


namespace prjcreamUCustomized.Controllers
{
    public class MaterialsController : Controller
    {
        private readonly CreamUdbContext econtext;
        public MaterialsController(CreamUdbContext db)//建構子
        {
            econtext = db;
        }
        public IActionResult MaterialList()
        {
            //table data to list
            var table = econtext.Materials.ToList();
            return View(table);
        }
    }
}
