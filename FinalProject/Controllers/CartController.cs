using Microsoft.AspNetCore.Mvc;
using FinalProject.Data;
using FinalProject.GVariable;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Controllers
{
    public class CartController : Controller
    {
        private readonly CreamUdbContext _eContext;
        public CartController(CreamUdbContext eecontext)
        {
            this._eContext = eecontext;
        }

        // cart page
        [HttpGet]
        public async Task<IActionResult> CartOrderDetail()
        {
            //cartlist cal total
            var cartlist = await _eContext.TempOrderDetails.ToListAsync();
            //TOTAL加減
            int tempt = 0;
            foreach (var cart in cartlist)
            {
                //add cal total
                tempt += Convert.ToInt32(cart.Subtotal);
            }
            //VALUE TO VIEWBAG
            ViewBag.Cart = Convert.ToString(tempt);
            return View(cartlist);
        }

        //save order and orderdetail data
        [ValidateAntiForgeryToken]
        public string save_order_and_orderdetail_data()
        {
            //find ORDER max id找出最大id 之後可以做加減
            PurchaseGVariable._Order_max_id = _eContext.Orders.Max(maxid => maxid.OrderId);
            PurchaseGVariable._Order_max_id++;
            //find ORDERDETAIL max id找出最大id 之後可以做加減
            PurchaseGVariable._OrderDetail_max_id = _eContext.OrderDetails.Max(maxid => maxid.OrderDetailId);
            PurchaseGVariable._OrderDetail_max_id++;
            //檢查是否成功的變數
            var checkcartpagevariable = new int();
            //新的order class 等等要存入資料固的order temp
            Order neworder = new Order();
            neworder = new Order()
            {
                OrderId = PurchaseGVariable._Order_max_id,
                EmployeeId = 1,
                MemberId = 1,
                OrderDate = System.DateTime.Now,
                TotalAmount = 123,
                OrderStatus = "0",
                PaymentStatus = "0",
                ShippingAddressId = null,
                OrderNotes = null
            };
            //temporderdetail to orderdetail
            foreach (var classtemp in _eContext.TempOrderDetails)
            {
                //把每筆TEMPORDERDETAIL站存至ORDERDETAIL
                //有些數值需轉換就轉換
                OrderDetail newtempoderdetail = new OrderDetail()
                {
                    OrderDetailId = PurchaseGVariable._OrderDetail_max_id,
                    OrderId = PurchaseGVariable._Order_max_id,
                    ProductId = classtemp.ProductId,
                    Qty = classtemp.Qty,
                    UnitPrice = classtemp.UnitPrice,
                    Discount = classtemp.Discount,
                    Subtotal = classtemp.Subtotal,
                    Notes = classtemp.Notes
                };
                //照資料庫結構DETAIL是要一直加一的
                PurchaseGVariable._OrderDetail_max_id++;
                //加入剛剛轉換完成的ORDERDETAIL
                _eContext.Add(newtempoderdetail);
            }
            //SAVE DATABASE CHANGE
            if (ModelState.IsValid)
            {
                _eContext.Add(neworder);
                _eContext.SaveChanges();
            }
            return PurchaseGVariable._Order_max_id.ToString() + " " +
                PurchaseGVariable._OrderDetail_max_id.ToString() + " " +
                Convert.ToString(checkcartpagevariable);
        }

        //postpage button method
        [HttpPost]
        public IActionResult PostAddressCreate(PostAddress papostaddress)
        {


            //load member address dat
            //判斷是否null如果沒執就設成一
            var hasData = _eContext.PostAddresses.Any();
            if (hasData)
            {
                PurchaseGVariable._PostAddress_max_id = _eContext.PostAddresses.Max(
                    maxid => maxid.AddressId);
                PurchaseGVariable._PostAddress_max_id++;
            }
            else
            {
                PurchaseGVariable._PostAddress_max_id = 1;
            }

            //save postaddress data
            if (ModelState.IsValid)
            {
                //maunal input data
                papostaddress.AddressId = PurchaseGVariable._PostAddress_max_id;
                _eContext.Add(papostaddress);
                _eContext.SaveChanges();

            }
            else
            {
                //maunal input data
                papostaddress.AddressId = PurchaseGVariable._PostAddress_max_id;
                papostaddress.MemberId = 1;
                papostaddress.AddressType = "2";
                papostaddress.RecipientName = "david";
                papostaddress.Address = "none";
                papostaddress.City = "k";
                papostaddress.Country = "t";
                papostaddress.PostalCode = "1";
                papostaddress.PhoneNumber = "09";
                papostaddress.IsDefault = 0;
                papostaddress.Notes = null;
                _eContext.Add(papostaddress);
                _eContext.SaveChanges();
            }

            return RedirectToAction("paymentpage");
        }

        //postaddress page 
        public IActionResult postaddresspage()
        {
            save_order_and_orderdetail_data();
            return View();
        }



        //PAYMENT PAGE
        public IActionResult paymentpage()
        {
            ViewBag.paymentpagetest = PurchaseGVariable._PostAddress_max_id;


            return View();
        }
    }
}
