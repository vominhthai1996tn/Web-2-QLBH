using System;
using System.Linq;
using System.Web.Mvc;
using WebDoAn.Filters;
using WebDoAn.Helpers;
using WebDoAn.Models;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace WebDoAn.Controllers
{
    [CheckLogin]
    public class CartController : Controller
    {
        //
        // GET: /Cart/
        public ActionResult Index()
        {
            var cart = CurrentContext.GetCart();
            
            return View(cart.Items);
        }

        //
        // POST: /Cart/Add
        [HttpPost]
        public ActionResult Add(int proId, int quantity, int proPrice)
        {          
            using(var ctx = new QLBHEntities())
            {
                var pro = ctx.Products
                    .Where(p => p.ProID == proId)
                    .FirstOrDefault();

                //pro.Price = pro.Price + 50000;
                //ctx.SaveChanges();

                var item = new CartItem
                {
                    Quantity = quantity,
                    Price = proPrice,
                    idUser = CurrentContext.GetCurUser().f_ID,
                    Product = pro
                };

                CurrentContext.moneyUser(item, CurrentContext.GetCurUser().f_ID);

                if (CurrentContext.GetCart().IsPrice(proId, proPrice) == false || item.moneyUser < proPrice)
                {
                    ViewBag.ErrorMsg = "Đã có người ra giá cao hơn giá của bạn. Mời bạn ra giá khác cao hơn.";
                   
                }
                else {
                    CurrentContext.GetCart().AddItem(item);

                    //var customer  = ctx.Users
                    //                .Where(p => p.f_ID == CurrentContext.GetCurUser().f_ID)
                    //                .FirstOrDefault();

                    //string content = System.IO.File.ReadAllText("~/assets/template/neworder.html");

                    //content = content.Replace("{{CustomerName}}", customer.f_Name);
                    //content = content.Replace("{{Phone}}", customer.f_Phone);
                    //content = content.Replace("{{Email}}", customer.f_Email);
                    //content = content.Replace("{{Address}}", customer.f_Address);
                    //content = content.Replace("{{ProName}}", item.Product.ProName);
                    //content = content.Replace("{{ProPrice}}", item.Price.ToString("N0"));
                    //content = content.Replace("{{Price}}", item.Product.Price.ToString("N0"));
                    //content = content.Replace("{{Number}}", item.Quantity.ToString());
                    //content = content.Replace("{{Deline}}", item.Product.DateTime.ToString());
                    //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                    //new MailHelper().SendMail("Đơn hàng từ Đấu giá QL", content);
                }
                
                return RedirectToAction("Detail", "Product", new { id = proId });
            }
            
        }
        //
        // POST: /Cart/Add2
        [HttpPost]
        public ActionResult Add2(int proId, int quantity, int curPage)
        {
            using (var ctx = new QLBHEntities())
            {
                var pro = ctx.Products
                    .Where(p => p.ProID == proId)
                    .FirstOrDefault();

                //pro.Price = pro.Price + 50000;
                //ctx.SaveChanges();
                
                var item = new CartItem
                {
                    Quantity = quantity,    
                    Price = pro.Price + 100000,
                    idUser = CurrentContext.GetCurUser().f_ID,
                    Product = pro
                };

                CurrentContext.moneyUser(item, CurrentContext.GetCurUser().f_ID);

                if (CurrentContext.GetCart().IsPrice(proId, Convert.ToInt32(pro.Price + 50000)) == false || item.moneyUser < pro.Price + 50000)
                {
                    ViewBag.ErrorMsg = "Đã có người ra giá cao hơn giá của bạn. Mời bạn ra giá khác cao hơn.";

                }
                else
                {
                    CurrentContext.GetCart().AddItem(item);
                }

                return RedirectToAction("ByCat", "Product", new { id = pro.CatID, page = curPage });
            }

        }

        //
        // POST: /Cart/Remove
        [HttpPost]
        public ActionResult Remove(int proId)
        {
            
                CurrentContext.GetCart().RemoveItem(proId);
                return RedirectToAction("Index", "Cart");   

        }

        // POST: /Cart/Update
        [HttpPost]
        public ActionResult Update(int proId, int proPrice)
        {

            //CurrentContext.GetCart().UpdateItem(proId, proPrice);
            return RedirectToAction("Detail", "Product");

        }
        
        // POST: /Cart/Checkout
        [HttpPost]
        public ActionResult Checkout()
        {
            using (var ctx = new QLBHEntities())
            {
                var ord = new Order
                {
                    OrderDate = DateTime.Now,
                    UserID = CurrentContext.GetCurUser().f_ID,
                    Total = 0
                };

                var c = CurrentContext.GetCart();
                foreach (var item in c.Items.Where(i => i.idUser == CurrentContext.GetCurUser().f_ID))
                {
                    var detail = new OrderDetail
                    {
                        ProID = item.Product.ProID,
                        //Quantity = item.Quantity,
                        Price = item.Product.Price,
                        Amount = item.Price
                    };

                    ord.OrderDetails.Add(detail);
                    ord.Total += detail.Amount;

                    //c.RemoveItem(item.Product.ProID);
                }

                ctx.Orders.Add(ord);
                ctx.SaveChanges();

            }

            CurrentContext.GetCart().Items.Clear();
            return RedirectToAction("Index", "Cart");

        }

        // GET: /Cart/
        public ActionResult Win(int idPro)
        {
            using (var ctx = new QLBHEntities())
            {                               
                var c = CurrentContext.GetCart();
                //tiem den san pham co ID = idPro, va user da dau gia no co id = id user hien tai da dang nhap
                var u = c.Items
                    .Where(i => i.idUser == CurrentContext.GetCurUser().f_ID && i.Product.ProID == idPro)
                    .FirstOrDefault();                               
               
                int idU = CurrentContext.GetCurUser().f_ID;
                //if (CurrentContext.GetCart().PriceMax(idU, idPro) == true)
                //{
                    var ord = new Order
                    {
                        OrderDate = DateTime.Now,
                        UserID = CurrentContext.GetCurUser().f_ID,
                        Total = u.Price//tổng giá đấu
                    };

                    var detail = new OrderDetail
                    {
                        ProID = u.Product.ProID,
                        Price = u.Product.Price,
                        Amount = u.Price//giá đấu
                    };

                    ord.OrderDetails.Add(detail);
                    //ord.Total += detail.Amount;

                    ctx.Orders.Add(ord);
                    ctx.SaveChanges();
                //}

                    //Remove(u.Product.ProID);//xoá sản phẩm đã thắng khỏi danh sách
            }
           
            //CurrentContext.GetCart().Items.Clear();
            return RedirectToAction("Index", "Cart");
        }      

	}
}