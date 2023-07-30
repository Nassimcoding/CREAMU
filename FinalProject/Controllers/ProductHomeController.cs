using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class ProductHomeController : Controller
    {
        private IWebHostEnvironment _enviro = null;
        public ProductHomeController(IWebHostEnvironment p)
        {
            _enviro = p;
        }
        public ActionResult demoFileUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult fileUploadDemo(IFormFile photo)
        {
            string path = _enviro.WebRootPath + "/imgs/001.jpg";
            photo.CopyTo(new FileStream(path, FileMode.Create));
            return View();
        }
    }
}
