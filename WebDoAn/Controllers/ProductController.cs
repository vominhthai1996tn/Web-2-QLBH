using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoAn.Models;

namespace WebDoAn.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/ByCat
        public ActionResult ByCat(int? id, int page = 1)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("Index", "Home");
            }

            using (var ctx = new QLBHEntities())
            {
                int n = ctx.Products
                    .Where(p => p.CatID == id)
                    .Count();

                int recordsPerPage = 9;
                int nPages = n / recordsPerPage;
                int m = n % recordsPerPage;
                if (m > 0)
                {
                    nPages++;
                }

                ViewBag.Pages = nPages;

                ViewBag.CurPage = page;
                ViewBag.CurPage = page;     

                var list = ctx.Products
                    .Where(p => p.CatID == id)
                    .OrderBy(p => p.ProID)
                    .Skip((page - 1 ) * recordsPerPage)
                    .Take(recordsPerPage)
                    .ToList();

                return View(list);
            }
            
        }
        //
        // GET: /Product/Detail
        public ActionResult Detail(int? id)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("Index", "Home");
            }

            using (var ctx = new QLBHEntities())
            {
                            
                var model = ctx.Products
                  .Where(p => p.ProID == id)
                  .FirstOrDefault();
                              
                return View(model);
            }

        }

        [HttpPost]
        public ActionResult Search(string keyword)
        {
            if (keyword != null)
            { 
                using (QLBHEntities ctx = new QLBHEntities())
                {
                Product model = ctx.Products
                    .Where(c => c.ProName == keyword.ToString())
                    .FirstOrDefault();

                return View(model);
                }
            }

            return View();
        }

        QLBHEntities db = new QLBHEntities();
        // GET: /Category/Search
        [HttpPost]
        public ActionResult SearchPro(FormCollection s)
        {
            string tukhoa = s["txtTimKiem"].ToString();
            List<Product> lstKQTK = db.Products.Where(c => c.ProName == tukhoa).ToList();

          
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm";
                
            }

            return View(lstKQTK.OrderBy(n => n.ProName));

            //if (name.HasValue == false)
            //{
            //    return RedirectToAction("Index", "Category");
            //}
            //using (QLBHEntities ctx = new QLBHEntities())
            //{
            //    Product model = ctx.Products
            //        .Where(c => c.ProName == name.ToString())
            //        .FirstOrDefault();

            //    return View(model);
            //}
            //return RedirectToAction("SearchPro", "Product");
        }
	}
}