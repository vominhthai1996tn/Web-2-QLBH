using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoAn.Models;

namespace WebDoAn.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/List
        public ActionResult List()
        {
            using (var ctx = new QLBHEntities())
            {
                var list = ctx.Categories.ToList();
                return PartialView("ListPartial", list);
            }
        }


        /*
        // GET: /Category/
        public ActionResult Index()
        {
            QLBHEntities ctx = new QLBHEntities();
            List<Category> list = ctx.Categories.ToList();

       
            return View(list);
        }

        // GET: /Category/Detail
        public ActionResult Detail(int? id)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("Index", "Category");
            }
            using (QLBHEntities ctx = new QLBHEntities())
            {
                Category model = ctx.Categories
                    .Where(c => c.CatID == id)
                    .FirstOrDefault();

                return View(model);
            }
        }

            // GET: /Category/Add
        public ActionResult Add()
        {      
            return View();
        }


        // POST: /Category/Add
        [HttpPost]
        public ActionResult Add(Category model)
        {
            using (var ctx = new QLBHEntities())
            {
                ctx.Categories.Add(model);
                ctx.SaveChanges();  
            }
            return View();
        }


        // GET: /Category/Delete
        public ActionResult Delete(int? id)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("Index", "Category");
            }

            ViewBag.ID = id;
            return View();
        }


        // POST: /Category/Delete
        [HttpPost]
        public ActionResult Delete(int idToDelete)
        {
            using (var ctx = new QLBHEntities())
            {
                Category model = ctx.Categories
                    .Where(c => c.CatID == idToDelete)
                    .FirstOrDefault();
                ctx.Categories.Remove(model);
                ctx.SaveChanges();
                        
            }
            return RedirectToAction("Index", "Category");
        }


        // GET: /Category/Edit
        public ActionResult Edit(int? id)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("Index", "Category");
            }
            using (var ctx = new QLBHEntities())
            {
                Category model = ctx.Categories
                    .Where(c => c.CatID == id)
                    .FirstOrDefault();


                return View(model);
            }
        }

        //
        // POST: /Category/Update
        [HttpPost]
        public ActionResult Update(Category model)
        {
            using (var ctx = new QLBHEntities())
            {

                ctx.Entry(model).State = 
                    System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();

            }
            return RedirectToAction("Index", "Category");
        }*/


    }
}