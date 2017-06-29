using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoAn.Helpers;
using WebDoAn.Filters;
using WebDoAn.Models;
using System.IO;

namespace WebDoAn.Controllers
{
    //[CheckLogin(RequiredPermission=1)]
    public class MProductController : Controller
    {
        
        //
        // GET: /MProduct/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /MProduct/Add
        public ActionResult Add()
        {
            using (var ctx = new QLBHEntities())
            {
                var list = ctx.Categories.ToList();
                ViewBag.Categories = list;
            }
            return View();
        }
          //
        // POST: /MProduct/Add
        [HttpPost]
        public ActionResult Add(Product vm, HttpPostedFileBase fuMain, HttpPostedFileBase fuThumbs)
        {
            using (var ctx = new QLBHEntities())
            {
                if (vm.TinyDes == null) vm.TinyDes = string.Empty;
                if (vm.FullDes == null) vm.FullDes = string.Empty;

                vm.IDSell = CurrentContext.GetCurUser().f_ID;

                ctx.Products.Add(vm);
                ctx.SaveChanges();

                if (fuMain != null && fuMain.ContentLength > 0 && fuThumbs != null && fuThumbs.ContentLength > 0)
                {
                    //tạo foder chứa hình
                    string spDirPath = Server.MapPath("~/Imgs/sp");
                    string targetDirPath = Path.Combine(spDirPath, vm.ProID.ToString());
                    Directory.CreateDirectory(targetDirPath);

                    //copy hình lên
                    string mainFileName = Path.Combine(targetDirPath, "main.jpg");
                    fuMain.SaveAs(mainFileName);

                    string thumbsFileName = Path.Combine(targetDirPath, "1.jpg");
                    fuThumbs.SaveAs(thumbsFileName);
                    string thumbsFileName1 = Path.Combine(targetDirPath, "2.jpg");
                    fuThumbs1.SaveAs(thumbsFileName1);
                }


                var list = ctx.Categories.ToList();
                ViewBag.Categories = list;
            }
            return View();
        }
	}
}
