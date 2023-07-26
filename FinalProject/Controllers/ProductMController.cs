﻿using FinalProject.Data;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


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

        public ActionResult ProductIndex(CKeywordViewModel vm, string keyword = "", decimal minPrice = 0, decimal maxPrice = decimal.MaxValue, int page = 1)
        {
            int pageSize = 10; //一頁顯示10樣商品
            IQueryable<Product> datas = SearchProducts(keyword, minPrice, maxPrice); //關鍵字＋價格篩選

            if (!string.IsNullOrEmpty(vm.txtKeyword))
                datas = datas.Where(p => p.ProductName.Contains(vm.txtKeyword));

            datas = datas.OrderByDescending(p => p.ReleaseDate); // 商品降序排列

            int recordCount = datas.Count();
            int totalPages = (int)Math.Ceiling((double)recordCount / pageSize);
            datas = datas.Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.Page = page; // 將當前頁數放進 ViewBag
            ViewBag.pageNum = totalPages;
            return View(datas.ToList());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");

            CreamUdbContext db = new CreamUdbContext();
            Product prod = db.Products.FirstOrDefault(p => p.ProductId == id);


            //獲取商品狀態的選項數據
            var productStatusOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "上架中", Text = "上架中" },
                new SelectListItem { Value = "缺貨中", Text = "缺貨中" },
                new SelectListItem { Value = "未上架", Text = "未上架" },
                new SelectListItem { Value = "已停產", Text = "已停產" }
            };
            ViewBag.Categories = productStatusOptions;
            return View(prod);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //用於防止跨站點請求偽造攻擊（CSRF）

        public ActionResult Edit(CProductWraper x, IFormFile file)
        {
            if (x == null)
                return RedirectToAction("List");

            CreamUdbContext db = new CreamUdbContext();
            Product prod = db.Products.FirstOrDefault(p => p.ProductId == x.ProductId);

            if (prod != null)
            {
                if (x.photo != null) //檢查是否上傳了新的圖片
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg"; //避免重複圖片名稱
                    x.photo.CopyTo(new FileStream(_enviro.WebRootPath + "/images/" + photoName,
                        FileMode.Create));
                    prod.ProductImage = photoName;
                }
                prod.UpdatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                prod.ProductName = x.ProductName;
                prod.Descript = x.Descript;
                prod.ProductStatus = x.ProductStatus;
                prod.CategoryId = x.CategoryId;
                prod.Price = x.Price;
                prod.ProductStock = x.ProductStock;
                prod.Type = x.Type;
                db.SaveChanges();
            }
            return RedirectToAction("ProductIndex");
        }

        public ActionResult Create()
        {
            SetProductStatusOptions();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product t, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if (photo == null)
                    ModelState.AddModelError("photo", "請選擇商品圖片");
                else if (!IsImageValid(photo))
                    ModelState.AddModelError("photo", "圖片格式不正確，只支援 JPG、PNG、GIF 格式。");

                if (IsProductDuplicate(t.ProductName))
                    ModelState.AddModelError("ProductName", "商品名稱已存在。");
                else if (t.ProductName.Length < 3)
                    ModelState.AddModelError("ProductName", "商品名稱必須至少包含3個字元。");
                else if (t.ProductName.Length > 60)
                    ModelState.AddModelError("ProductName", "商品名稱不能超過60個字元。");

                if (t.ProductStock <= 0)
                    ModelState.AddModelError("ProductStock", "庫存數量必須大於零。");

                if (t.Price <= 0)
                    ModelState.AddModelError("Price", "價格必須大於零。");

                if (ModelState.IsValid)
                {
                    t.ReleaseDate = DateTime.Now.ToString("yyyy-MM-dd"); // 設定 ReleaseDate 為當下日期，紀錄修改商品資訊的日期

                    if (photo != null) // 上傳圖片
                    {
                        string photoName = Guid.NewGuid().ToString() + ".jpg";
                        string imagePath = Path.Combine(_images.WebRootPath, "images", photoName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                            photo.CopyTo(stream);
                        t.ProductImage = photoName;
                    }
                    CreamUdbContext db = new CreamUdbContext();
                    db.Products.Add(t);
                    db.SaveChanges();
                    return RedirectToAction("ProductIndex");
                }
            }
            // 驗證失敗
            SetProductStatusOptions();
            return View(t);
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
            return RedirectToAction("ProductIndex");
        }

        // 查詢
        private IQueryable<Product> SearchProducts(string keyword, decimal minPrice, decimal maxPrice)
        {
            CreamUdbContext db = new CreamUdbContext();
            IQueryable<Product> datas = db.Products;

            if (!string.IsNullOrEmpty(keyword))
                datas = datas.Where(p => p.ProductName.Contains(keyword));

            datas = datas.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
            return datas;
        }

        // 商品預覽
        public ActionResult ProductDetail(int id)
        {
            CreamUdbContext db = new CreamUdbContext();
            Product product = db.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return NotFound(); //如果找不到對應的商品，可以返回一個404頁面或其他適當的處理方式

            // 商品資料轉換 JSON 
            var jsonResult = new
            {
                imageUrl = product.ProductImage,
                productName = product.ProductName,
                price = product.Price,
                description = product.Descript,
                productStock = product.ProductStock
            };
            return Json(jsonResult);
        }

        private void SetProductStatusOptions()
        {
            var productStatusOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "上架中", Text = "上架中" },
            new SelectListItem { Value = "缺貨中", Text = "缺貨中" },
            new SelectListItem { Value = "暫時下架", Text = "暫時下架" },
            new SelectListItem { Value = "已停產", Text = "已停產" }
        };
            ViewBag.Categories = productStatusOptions;
        }

        private bool IsProductDuplicate(string productName)
        {
            CreamUdbContext db = new CreamUdbContext();
            return db.Products.Any(p => p.ProductName == productName);
        }

        private bool IsImageValid(IFormFile photo)
        {
            // 支援的圖片格式
            string[] supportedFormats = { "jpg", "jpeg", "png", "gif" };

            // 獲取圖片的檔案擴展名
            var fileExtension = Path.GetExtension(photo.FileName).Substring(1).ToLower();

            // 檢查檔案擴展名是否在支援的格式列表中
            return supportedFormats.Contains(fileExtension);
        }
    }
}
