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
    public class WinProductController : Controller
    {
        //
        // GET: /WinProduct/
        public ActionResult Index()
        {
            var winpro = CurrentContext.GetWinPro();
            
            return View(winpro.ItemsOrderDetail);            
        }

        //GET://ListWinProduct
        public ActionResult ListWinProduct()
        {
            using (var ctx = new QLBHEntities())
            {
                var list = ctx.OrderDetails                  
                    .ToList();               

                return View(list);
            }          
        }

        // GET: /List ID Order/
        public ActionResult ListIDOder()
        {
            using (var ctx = new QLBHEntities())
            {
                var list = ctx.Orders
                    .Where(o => o.UserID == CurrentContext.GetCurUser().f_ID)
                    .ToList();

                ViewBag.Listidoder = list;

                return View("ListWinProduct", ViewBag.Listidoder);
            }
        }
	}
}