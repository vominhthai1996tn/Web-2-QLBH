using BotDetect.Web.Mvc;
using System;
using System.Linq;
using System.Web.Mvc;
using WebDoAn.Filters;
using WebDoAn.Helpers;
using WebDoAn.Models;


namespace WebDoAn.Controllers
{
    
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }
        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            string encPwd = StringUtils.Md5(model.RawPWD);
            using (QLBHEntities ctx = new QLBHEntities())
            {
                var user = ctx.Users
                    .Where(u => u.f_Username == model.Username && u.f_Password == encPwd)
                    .FirstOrDefault();

                if (user != null) {

                    Session["isLogin"] = 1;
                    Session["user"] = user;
                    if (model.Remember) {
                        Response.Cookies["userId"].Value = user.f_ID.ToString();
                        Response.Cookies["userId"].Expires = DateTime.Now.AddDays(7);
                    }

                    ViewBag.iduser = user.f_ID;
                    //ViewBag.IDLogin = user.f_ID.ToString();
                    //ViewBag.TK = Convert.ToInt32(user.f_Money);
                   
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMsg = "Đăng nhập thất bại.";
                    return View();
                }
            }
          
        }
        //
        // POST: /Account/Logout
        [HttpPost]
        public ActionResult Logout()
        {
            CurrentContext.Destroy();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]

        [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: Captcha validation failed, show error message

                ViewBag.ErrorMsg = "Sai mã xác nhận, vui lòng nhập lại!";
            }
            else
            {
             
                // TODO: Captcha validation passed, proceed with protected action

                User u = new User
                {
                    f_Username = model.Username,
                    f_Email = model.Email,
                    f_Name = model.Name,
                    f_Password = StringUtils.Md5(model.RawPWD),
                    f_Permission = 0,
                    f_DOB = DateTime.ParseExact(model.DOB, "d/m/yyyy", null),
                    //f_Money = Decimal.Parse(model.Money, NumberStyles.Currency)
                    f_Money = Convert.ToDecimal(model.Money.ToString().TrimStart('¥'))

                };

                using (QLBHEntities ctx = new QLBHEntities())
                {
                    ctx.Users.Add(u);
                    ctx.SaveChanges();
                }
                
            }
            return View();
        }
        //
        // GET: /Account/Profile
        [CheckLogin]
        public ActionResult Profile(int? id)
        {
            using (var ctx = new QLBHEntities())
            {
                var user = ctx.Users
                              .Where(u => u.f_ID == id)
                              .FirstOrDefault();

                return View(user);
            }
           
        }

        //
        // POST: /Account/EditName   
         [HttpPost]
        public ActionResult EditName(string nameUser, int? id)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("Profile", "Account");
            }

            if (nameUser != "" && nameUser != null)
            {
                using (var ctx = new QLBHEntities())
                {
                    //tim den thong tin user
                    var user = ctx.Users
                        .Where(i => i.f_ID == id)
                        .FirstOrDefault();

                    // user ton tai, loi
                    if (user != null)
                    {                        
                        user.f_Name = nameUser;

                        ctx.Entry(user).State =
                            System.Data.Entity.EntityState.Modified;                       
                    
                        ctx.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Profile", "Account", new { id = CurrentContext.GetCurUser().f_ID });
        }

        //
        // POST: /Account/EditEmail  
         [HttpPost]
         public ActionResult EditEmail(string emailUser, int? id)
         {
            if (id.HasValue == false)
            {
                return RedirectToAction("Profile", "Account");
            }

            if (emailUser != "" && emailUser != null)
            {
                using (var ctx = new QLBHEntities())
                {
                    //tim den thong tin user
                    var user = ctx.Users
                        .Where(i => i.f_ID == id)
                        .FirstOrDefault();

                    // user ton tai, loi
                    if (user != null)
                    {
                        user.f_Email = emailUser;

                        ctx.Entry(user).State =
                            System.Data.Entity.EntityState.Modified;                       
                    
                        ctx.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Profile", "Account", new { id = CurrentContext.GetCurUser().f_ID });
        }

         //
         // POST: /Account/EditPassword  
         [HttpPost]
         public ActionResult EditPassword(string pwUser, int? id)
         {
             if (id.HasValue == false)
             {
                 return RedirectToAction("Profile", "Account");
             }

             if (pwUser != "" && pwUser != null)
             {
                 using (var ctx = new QLBHEntities())
                 {
                     //tim den thong tin user
                     var user = ctx.Users
                         .Where(i => i.f_ID == id)
                         .FirstOrDefault();

                     // user ton tai, loi
                     if (user != null)
                     {
                         user.f_Password = StringUtils.Md5(pwUser);

                         ctx.Entry(user).State =
                             System.Data.Entity.EntityState.Modified;

                         ctx.SaveChanges();
                     }
                 }
             }

             return RedirectToAction("Profile", "Account", new { id = CurrentContext.GetCurUser().f_ID });
         }
	}
}