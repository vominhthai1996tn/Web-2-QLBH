using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoAn.Helpers;
using WebDoAn.Models;

namespace WebDoAn.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index1()
        {
            var topmax = CurrentContext.GetMaxPrice();           
            return View(topmax.ProMax);          
        }

        //
        // GET//Top 5 san pham gia cao nhat
        public ActionResult Index()
        {
            using (var ctx = new QLBHEntities())
            {
                var list = ctx.Products.OrderByDescending(l => l.Price).ToList();
                Deline();
                return View("Index", list);
            }
            
        }

        //
        // GET//Top 5 san pham gia gan ket thuc dau gia
        public ActionResult Deline()
        {
            using (var ctx = new QLBHEntities())
            {
                var list = ctx.Products.OrderByDescending(l => l.DateTime).ToList();
                list.Reverse();
                ViewBag.ListDeline = list;
                return View("Index", ViewBag.ListDeline);
            }

        }

    }
}
