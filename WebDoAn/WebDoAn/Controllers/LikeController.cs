using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoAn.Helpers;
using WebDoAn.Models;

namespace WebDoAn.Controllers
{
    public class LikeController : Controller
    {
        //
        // GET: /Like/
        public ActionResult Index()
        {
            var cart = CurrentContext.GetToLike();

            return View(cart.ItemsLike);
        }

        //
        // POST: /Cart/Like
        [HttpPost]
        public ActionResult Like(int proId, int quantity, int curPage)
        {
            using (var ctx = new QLBHEntities())
            {
                var pro = ctx.Products
                    .Where(p => p.ProID == proId)
                    .FirstOrDefault();

                var item = new LikeItem
                {
                    Quantity = quantity,
                    idUser = CurrentContext.GetCurUser().f_ID,
                    Product = pro
                };

                CurrentContext.GetToLike().AddLikeItem(item);
                return RedirectToAction("ByCat", "Product", new { id = pro.CatID, page = curPage });
            }

        }

        //
        // POST: /Cart/Remove
        [HttpPost]
        public ActionResult RemoveLike(int proId)
        {

            CurrentContext.GetToLike().RemoveLikeItem(proId);
            return RedirectToAction("Index", "Like");

        }
	}
}