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
                    f_DOB = DateTime.ParseExact(model.DOB, "d/m/yyyy", null)


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
        public ActionResult Profile()
        {
            
            return View();
        }
        
	}
    
}