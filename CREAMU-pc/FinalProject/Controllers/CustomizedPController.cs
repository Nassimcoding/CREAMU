using Microsoft.AspNetCore.Mvc;
using FinalProject.Data;
using FinalProject.ViewModel;
using System.Text.Json;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class CustomizedPController : Controller
    {
        
        public IActionResult CustomizedProd()
        {
            CreamUdbContext db = new CreamUdbContext();
            var materials = from m in db.Materials
                            select m;
            var models = from M in db.Models
                         select M;
            var head = from M in db.Models
                       where M.ModelType == "Head"
                       select M;
            var body = from M in db.Models
                       where M.ModelType == "Body"
                       select M;
            var rh = from M in db.Models
                     where M.ModelType == "RH"
                     select M;
            var lh = from M in db.Models
                     where M.ModelType == "LH"
                     select M;
            var rf = from M in db.Models
                     where M.ModelType == "RF"
                     select M;
            var lf = from M in db.Models
                     where M.ModelType == "LF"
                     select M;
            var ModandMat = new CCustomizedProdViewModel
            {
                Materials = materials,
                Models = models,
                Head = head,
                Body = body,
                RH = rh,
                LH = lh,
                RF = rf,
                LF = lf
            };
            ViewBag.HeadID = 123;
            ViewBag.BodyID = 55555;
            return View(ModandMat);
        }
        [HttpPost]
        public ActionResult CustomizedProd(CCPAddToCartViewMode vm)
        {
            CreamUdbContext db = new CreamUdbContext();
            //找到ComponentID的資料
            Data.Component comHead = db.Components.FirstOrDefault(c => c.ModelId == vm.HeadID && c.MaterialId ==vm.HeadID_M);
            Data.Component comBody = db.Components.FirstOrDefault(c => c.ModelId == vm.BodyID && c.MaterialId == vm.BodyID_M);
            Data.Component comRH = db.Components.FirstOrDefault(c => c.ModelId == vm.RHID && c.MaterialId == vm.RHID_M);
            Data.Component comLH = db.Components.FirstOrDefault(c => c.ModelId == vm.LHID && c.MaterialId == vm.LHID_M);
            Data.Component comRF = db.Components.FirstOrDefault(c => c.ModelId == vm.RFID && c.MaterialId == vm.RFID_M);
            Data.Component comLF = db.Components.FirstOrDefault(c => c.ModelId == vm.LFID && c.MaterialId == vm.LFID_M);
            
            ViewBag.HeadCom = comHead;
            ViewBag.BodyCom = comBody;
            ViewBag.ComRH = comRH;
            ViewBag.ComLH = comLH;
            ViewBag.ComRF = comRF;
            ViewBag.ComLF = comLF;
                

            //計算總價
            
            int totalPrice = 0;
            Model model;
            model = db.Models.FirstOrDefault(mp => mp.ModelId == vm.HeadID);
            if (model != null)
            {
                totalPrice += model.Price;
            }
            model = db.Models.FirstOrDefault(mp => mp.ModelId == vm.BodyID);
            if (model != null)
            {
                totalPrice += model.Price;
            }
            model = db.Models.FirstOrDefault(mp => mp.ModelId == vm.RHID);
            if (model != null)
            {
                totalPrice += model.Price;
            }
            model = db.Models.FirstOrDefault(mp => mp.ModelId == vm.LHID);
            if (model != null)
            {
                totalPrice += model.Price;
            }
            model = db.Models.FirstOrDefault(mp => mp.ModelId == vm.RFID);
            if (model != null)
            {
                totalPrice += model.Price;
            }
            model = db.Models.FirstOrDefault(mp => mp.ModelId == vm.LFID);
            if (model != null)
            {
                totalPrice += model.Price;
            }
            Material material;
            material = db.Materials.FirstOrDefault(mp => mp.MaterialId == vm.HeadID_M);
            if (material != null)
            {
                totalPrice += material.Price;
            }
            material = db.Materials.FirstOrDefault(mp => mp.MaterialId == vm.BodyID_M);
            if (material != null)
            {
                totalPrice += material.Price;
            }
            material = db.Materials.FirstOrDefault(mp => mp.MaterialId == vm.RHID_M);
            if (material != null)
            {
                totalPrice += material.Price;
            }
            material = db.Materials.FirstOrDefault(mp => mp.MaterialId == vm.LHID_M);
            if (material != null)
            {
                totalPrice += material.Price;
            }
            material = db.Materials.FirstOrDefault(mp => mp.MaterialId == vm.RFID_M);
            if (material != null)
            {
                totalPrice += material.Price;
            }
            material = db.Materials.FirstOrDefault(mp => mp.MaterialId == vm.LFID_M);
            if (material != null)
            {
                totalPrice += material.Price;
            }




            //存到json
            /*if (comHead != null)
            {
                string json = "";
                List<CCPShoppingCartItem> cart = null;
                if (HttpContext.Session.Keys.Contains(CCPDictionary.SK_PURCHASED_CUSTOMIZEDPRODUCTS_LIST))
                {
                    json = HttpContext.Session.GetString(CCPDictionary.SK_PURCHASED_CUSTOMIZEDPRODUCTS_LIST);
                    cart = JsonSerializer.Deserialize<List<CCPShoppingCartItem>>(json);
                }
                else
                    cart = new List<CCPShoppingCartItem>();
                CCPShoppingCartItem item = new CCPShoppingCartItem();
                item.HeadCom = comHead.ComponentId;
                item.BodyCom = comBody.ComponentId;
                item.RHCom = comRH.ComponentId;
                item.LHCom = comLH.ComponentId;
                item.RFCom = comRF.ComponentId;
                item.LFCom = comLF.ComponentId;
                item.count = vm.count;
                item.SubTota = totalPrice;
                //item.SubTota = //缺單價
                //item.TotalP = //缺單價*數量
                cart.Add(item);
                json = JsonSerializer.Serialize(cart);
                HttpContext.Session.SetString(CCPDictionary.SK_PURCHASED_CUSTOMIZEDPRODUCTS_LIST, json);
            }*/
            

            ViewBag.HeadID = vm.HeadID;
            ViewBag.BodyID = vm.BodyID;
            ViewBag.RHID = vm.RHID;
            ViewBag.LHID = vm.LHID;
            ViewBag.RFID = vm.RFID;
            ViewBag.LFID = vm.LFID;

            ViewBag.HeadID_M = vm.HeadID_M;
            ViewBag.BodyID_M = vm.BodyID_M;
            ViewBag.RHID_M = vm.RHID_M;
            ViewBag.LHID_M = vm.LHID_M;
            ViewBag.RFID_M = vm.RFID_M;
            ViewBag.LFID_M = vm.LFID_M;

            ViewBag.count = vm.count;
            ViewBag.price = totalPrice;

            ViewBag.HeadCom = comHead.ComponentId;
            ViewBag.BodyCom = comBody.ComponentId;
            ViewBag.ComRH = comRH.ComponentId;
            ViewBag.ComLH = comLH.ComponentId;
            ViewBag.ComRF = comRF.ComponentId;
            ViewBag.ComLF = comLF.ComponentId;
            return View("CPCartView", vm);
        }
        public ActionResult CPCartView(CCPAddToCartViewMode vm)
        {

            ViewBag.HeadID = vm.HeadID;
            ViewBag.BodyID = vm.BodyID;
            ViewBag.RHID = vm.RHID;
            ViewBag.LHID = vm.LHID;
            ViewBag.RFID = vm.RFID;
            ViewBag.LFID = vm.LFID;

            ViewBag.HeadID_M = vm.HeadID_M;
            ViewBag.BodyID_M = vm.BodyID_M;
            ViewBag.RHID_M = vm.RHID_M;
            ViewBag.LHID_M = vm.LHID_M;
            ViewBag.RFID_M = vm.RFID_M;
            ViewBag.LFID_M = vm.LFID_M;

            ViewBag.count = vm.count;
            ViewBag.price = vm.price;

            return View();
        }
        public IActionResult CartView()//還沒做
        {
            if (!HttpContext.Session.Keys.Contains(CCPDictionary.SK_PURCHASED_CUSTOMIZEDPRODUCTS_LIST))
                return RedirectToAction("List");
            string json = HttpContext.Session.GetString(CCPDictionary.SK_PURCHASED_CUSTOMIZEDPRODUCTS_LIST);
            List<CCPShoppingCartItem> cart = JsonSerializer.Deserialize<List<CCPShoppingCartItem>>(json);

            if (cart == null)
                return RedirectToAction("List");
            return View(cart);
        }


        //查Model的價格
        public IActionResult GetModPrice(int id)
        {
            var db = new CreamUdbContext();
            var model = db.Models.FirstOrDefault(m => m.ModelId == id);

            if (model != null)
            {
                var price = model.Price;
                return Content(price.ToString()); 
            }

            return Content("Price not found");
        }
        //查Material的價格
        public IActionResult GetMatPrice(int id)
        {

            var db = new CreamUdbContext();
            var model = db.Materials.FirstOrDefault(m => m.MaterialId == id);

            if (model != null)
            {
                var price = model.Price;
                return Content(price.ToString()); 
            }

            return Content("Price not found");
        }
    }
}
