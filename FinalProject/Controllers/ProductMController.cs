using FinalProject.Data;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Controllers
{
    public class ProductMController : Controller
    {
        // 宣告私有變數用於儲存環境設定
        private IWebHostEnvironment _enviro = null;
        private IWebHostEnvironment _images;

        // 建構子，注入環境設定
        public ProductMController(IWebHostEnvironment p, IWebHostEnvironment images)
        {
            _enviro = p;
            _images = images;
        }

        //顯示商品清單並支援篩選功能
        public ActionResult ProductIndex(CKeywordViewModel vm, string keyword = "", string productStatus = "all", decimal minPrice = 0, decimal maxPrice = decimal.MaxValue, int page = 1)
        {
            // 分頁設定
            int pageSize = 10;
            IQueryable<Product> datas = SearchProducts(keyword, minPrice, maxPrice);

            // 根據選擇的商品狀態篩選商品
            if (!string.IsNullOrEmpty(productStatus) && productStatus != "all")
            {
                datas = datas.Where(p => p.ProductStatus == productStatus);
            }

            // 根據用戶輸入的關鍵字進行篩選
            if (!string.IsNullOrEmpty(vm.txtKeyword))
            {
                datas = datas.Where(p => p.ProductName.Contains(vm.txtKeyword));
            }

            // 商品按照發布日期降序排列
            datas = datas.OrderByDescending(p => p.ReleaseDate);

            // 執行分頁以顯示特定頁面的商品
            int recordCount = datas.Count();
            int totalPages = (int)Math.Ceiling((double)recordCount / pageSize);
            datas = datas.Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.Page = page; // 當前頁數
            ViewBag.pageNum = totalPages; // 總頁數
            return View(datas.ToList());
        }

        //顯示特定商品的編輯視圖
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List"); // 如果未提供商品 ID，則重定向到列表頁面

            CreamUdbContext db = new CreamUdbContext();
            Product prod = db.Products.FirstOrDefault(p => p.ProductId == id);


            // 創建商品狀態的下拉選項數據
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
        // HTTP POST 動作：處理商品編輯表單提交
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
                    x.photo.CopyTo(new FileStream(_enviro.WebRootPath + "/imgs/" + photoName,
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
            try
            {
                if (ModelState.IsValid)
                {
                    // 驗證表單數據的有效性
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

                    // 如果驗證通過，則執行商品創建邏輯
                    if (ModelState.IsValid)
                    {
                        t.ReleaseDate = DateTime.Now.ToString("yyyy-MM-dd"); // 設定 ReleaseDate 為當下日期，紀錄修改商品資訊的日期

                        if (photo != null) // 上傳圖片
                        {
                            string photoName = Guid.NewGuid().ToString() + ".jpg";
                            string imagePath = Path.Combine(_images.WebRootPath, "imgs", photoName);
                            using (var stream = new FileStream(imagePath, FileMode.Create))
                                photo.CopyTo(stream);
                            t.ProductImage = photoName;
                        }

                        // 儲存商品資料到資料庫
                        CreamUdbContext db = new CreamUdbContext();
                        db.Products.Add(t);
                        db.SaveChanges();

                        return RedirectToAction("ProductIndex");
                    }
                }

                // 驗證失敗，重新顯示創建商品的視圖，並傳遞錯誤訊息給使用者
                SetProductStatusOptions();
                return View(t);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "資料庫存取異常，請稍後再試或聯絡系統管理員。");
                return View(t);
            }
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


        //加入資料庫存取異常處理
        private IQueryable<Product> SearchProducts(string keyword, decimal minPrice, decimal maxPrice)
        {
            try
            {
                CreamUdbContext db = new CreamUdbContext();
                IQueryable<Product> datas = db.Products;

                if (!string.IsNullOrEmpty(keyword))
                    datas = datas.Where(p => p.ProductName.Contains(keyword));

                datas = datas.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
                return datas;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "資料庫存取異常，請稍後再試或聯絡系統管理員。");
                throw; // 可以選擇重新拋出異常，讓上層處理異常
            }
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

        // 輔助方法：檢查指定商品名稱是否已存在於資料庫
        private bool IsProductDuplicate(string productName)
        {
            CreamUdbContext db = new CreamUdbContext();
            return db.Products.Any(p => p.ProductName == productName);
        }

        // 輔助方法：檢查上傳的圖片是否為有效格式（JPG、JPEG、PNG、GIF）
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
