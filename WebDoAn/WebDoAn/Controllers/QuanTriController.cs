using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoAn.Helpers;
using WebDoAn.Models;

namespace WebDoAn.Controllers
{
    public class QuanTriController : Controller
    {
        //
        // GET: /QuanTri/
        public ActionResult Index()
        {
            var cart = CurrentContext.GetQuanTriVien();

            return View(cart.ItemsUsers);
        }

        //danh sach nguoi dung
        public ActionResult UserList ()
        {
            using (var ctx = new QLBHEntities())
            {
                var list = ctx.Users
                              .ToList();               

                return View(list);
            }          
        }

        [HttpPost]
        public ActionResult RemoveUser(int userId)
         {
            
              //CurrentContext.GetQuanTriVien().RemoveItemsUsers(userId);

             using (var ctx = new QLBHEntities())
             {
                 User model = ctx.Users
                     .Where(c => c.f_ID == userId)
                     .FirstOrDefault();

                 if (model != null)
                 {
                     ctx.Users.Remove(model);
                     ctx.SaveChanges();
                 }
             }

             return RedirectToAction("UserList", "QuanTri");   
        }

	}
}