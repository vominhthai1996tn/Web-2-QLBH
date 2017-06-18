using System;
using System.Linq;
using System.Web.Mvc;
using WebDoAn.Filters;
using WebDoAn.Helpers;
using WebDoAn.Models;
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
        public ActionResult Add(int proId, int quantity)
        {
            using(var ctx = new QLBHEntities())
            {
                var pro = ctx.Products
                    .Where(p => p.ProID == proId)
                    .FirstOrDefault();

                var item = new CartItem
                {
                    Quantity = quantity,
                    Product = pro
                };

                CurrentContext.GetCart().AddItem(item);

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

                var item = new CartItem
                {
                    Quantity = quantity,
                    Product = pro
                };

                CurrentContext.GetCart().AddItem(item);

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
        //
        // POST: /Cart/Update
        [HttpPost]
        public ActionResult Update(int proId, int quantity)
        {

            CurrentContext.GetCart().UpdateItem(proId, quantity);
            return RedirectToAction("Index", "Cart");

        }
        //
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
                foreach (var item in c.Items)
                {
                    var detail = new OrderDetail
                    {
                        ProID = item.Product.ProID,
                        Quantity = item.Quantity,
                        Price = item.Product.Price,
                        Amount = item.Product.Price * item.Quantity
                    };
                    ord.OrderDetails.Add(detail);
                    ord.Total += detail.Amount;
                }
                ctx.Orders.Add(ord);
                ctx.SaveChanges();

            }
            CurrentContext.GetCart().Items.Clear();
            return RedirectToAction("Index", "Cart");

        }
	}
}