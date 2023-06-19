using Microsoft.AspNetCore.Mvc;
using FinalProject.Data;

namespace prjcreamUCustomized.Controllers
{
    public class CombineDetailsController : Controller
    {
        private readonly CreamUdbContext econtext;
        public CombineDetailsController(CreamUdbContext db)//建構子
        {
            econtext = db;
        }
        public IActionResult CombineDetailList()
        {
            //table data to list
            var table = econtext.CombineDetails.ToList();
            return View(table);
        }
    }
}
