using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoAn.Helpers;
using WebDoAn.Filters;
using WebDoAn.Models;

namespace WebDoAn.Controllers
{
    public class ListProductController : Controller
    {
        //
        // GET: /ListProduct/
        public ActionResult Index()
        {
            var listpro = CurrentContext.GetListPro();

            return View(listpro.ItemsListPro);
        }

       
        public ActionResult ListAuction()
        {
            using (var ctx = new QLBHEntities())
            {
                var list = ctx.Products                   
                    .Where(c => c.DateTime >= DateTime.Now)
                              .ToList();

                return View(list);
            }           
        }

        //sản phẩm đấu giá xong, hết bạn đấu giá
        public ActionResult ListDeline()
        {
            using (var ctx = new QLBHEntities())
            {
                var list = ctx.Products
                    .Where(c => c.DateTime < DateTime.Now)
                              .ToList();

                return View(list);
            }
        }
	}
}