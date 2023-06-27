using FinalProject.Data;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Controllers
{
    public class ProductMController : Controller
    {
        private IWebHostEnvironment _enviro = null;
        private IWebHostEnvironment _images;

        public ProductMController(IWebHostEnvironment p, IWebHostEnvironment images)
        {
            _enviro = p;
            _images = images;
        }







        public ActionResult ProductIndex(CKeywordViewModel vm, string KeyWord = "", int Page = 1)
        {
            int pageSize = 10;

            string keyword = vm.txtKeyword;
            CreamUdbContext db = new CreamUdbContext();
            IQueryable<Product> datas = null;

            if (string.IsNullOrEmpty(keyword))
                datas = db.Products;
            else
                datas = db.Products.Where(p => p.ProductName.Contains(keyword));

            int recordCount = datas.Count();
            int totalPages = (int)Math.Ceiling((double)recordCount / pageSize);

            // 分頁功能
            datas = datas.Skip((Page - 1) * pageSize).Take(pageSize);
            ViewBag.pageNum = totalPages;
            return View(datas.ToList());
        }

        public ActionResult DisplayOnSaleProducts()
        {
            CreamUdbContext db = new CreamUdbContext();
            List<Product> products = db.Products.Where(p => p.ProductStatus == "上架中").ToList();
            return View(products);
        }
        public ActionResult DisplayOffSaleProducts()
        {
            CreamUdbContext db = new CreamUdbContext();
            Product prod = (Product)db.Products.Where(p => p.ProductStatus == "下架中");
            return View(prod);
        }







        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("ProductIndex");
            CreamUdbContext db = new CreamUdbContext();
            Product prod = db.Products.FirstOrDefault(p => p.ProductId == id);

            // 獲取商品狀態的選項數據
            var productStatusOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "上架中", Text = "上架中" },
                new SelectListItem { Value = "缺貨中", Text = "缺貨中" },
                new SelectListItem { Value = "暫時下架", Text = "暫時下架" },
                new SelectListItem { Value = "已停產", Text = "已停產" }
            };
            ViewBag.Categories = productStatusOptions;
            return View(prod);
        }
        [HttpPost]
        public ActionResult Edit(CProductWraper x, IFormFile file)
        {
            CreamUdbContext db = new CreamUdbContext();
            Product prod = db.Products.FirstOrDefault(p => p.ProductId == x.ProductId);
            if (prod != null)
            {
                if (x.photo != null && file.Length > 0)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    x.photo.CopyTo(new FileStream(_enviro.WebRootPath + "/images/" + photoName,
                        FileMode.Create));
                    prod.ProductImage = photoName; 
                }

                // 設定 ReleaseDate 為當下日期
                prod.UpdatedDate = DateTime.Now.ToString("yyyy-MM-dd");

                prod.ProductName = x.ProductName; //名稱
                prod.Descript = x.Descript; //商品敘述
                prod.ProductStatus = x.ProductStatus; //商品狀態
                prod.CategoryId = x.CategoryId; //類型id ------ f.k 到類型 (紀錄 : 可愛動物、兇猛動物..)
                prod.Price = x.Price; //售價
                prod.ProductStock = x.ProductStock; //庫存
                prod.Type = x.Type; //類型(成品&客製)
                db.SaveChanges();
            }
            return RedirectToAction("ProductIndex");
        }









        public ActionResult Create()
        {
            var productStatusOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "上架中", Text = "上架中" },
                new SelectListItem { Value = "缺貨中", Text = "缺貨中" },
                new SelectListItem { Value = "暫時下架", Text = "暫時下架" },
                new SelectListItem { Value = "已停產", Text = "已停產" }
            };
            ViewBag.Categories = productStatusOptions;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product t, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                // 檢查商品名稱是否重複
                if (IsProductDuplicate(t.ProductName))
                {
                    ModelState.AddModelError("ProductName", "商品名稱已存在。");
                    return View(t);
                }
                // 設定 ReleaseDate 為當下日期
                t.ReleaseDate = DateTime.Now.ToString("yyyy-MM-dd");
                // 處理上傳圖片的邏輯
                if (photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    string imagePath = Path.Combine(_images.WebRootPath, "images", photoName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }
                    t.ProductImage = photoName;
                }
                // 保存新建的商品到資料庫
                CreamUdbContext db = new CreamUdbContext();
                db.Products.Add(t);
                db.SaveChanges();

                return RedirectToAction("List");
            }
            return View(t);
        }

        private bool IsProductDuplicate(string productName)
        {
            // 檢查商品名稱是否重複的邏輯
            CreamUdbContext db = new CreamUdbContext();
            return db.Products.Any(p => p.ProductName == productName);
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                CreamUdbContext db = new CreamUdbContext();
                Product prod = db.Products.FirstOrDefault(p => p.ProductId == id);
                if (prod != null)
                {
                    db.Products.Remove(prod);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }




    }
}
