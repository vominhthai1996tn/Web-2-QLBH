using System.Web;
using WebDoAn.Models;
using WebDoAn.Filters;
using System;
using System.Linq;

namespace WebDoAn.Helpers
{
    public class CurrentContext
    {
        public static bool IsLogged()
        {
            var flag = HttpContext.Current.Session["isLogin"];
            if (flag == null || (int)flag == 0)
            {
                if (HttpContext.Current.Request.Cookies["userId"] != null)
                {
                    int userIdCookie = Convert.ToInt32(HttpContext.Current.Request.Cookies["userId"].Value);

                    using (QLBHEntities ctx = new QLBHEntities())
                    {

                        var user = ctx.Users
                        .Where(u => u.f_ID == userIdCookie)
                        .FirstOrDefault();

                        HttpContext.Current.Session["isLogin"] = 1;
                        HttpContext.Current.Session["user"] = user;
                        HttpContext.Current.Response.Cookies["userId"].Expires =
                                DateTime.Now.AddDays(-1);
                    }
                    return true;

                }
                return false;
            }
            return true;
        }

        public  static User GetCurUser()
        {
            return (User)HttpContext.Current.Session["user"];
        }

        public static Cart GetCart()
        {
            var ret = (Cart)HttpContext.Current.Session["cart"];
            if (ret == null)
            {
                ret = new Cart();
                HttpContext.Current.Session["cart"] = ret;
                
            }
                        
            
            return ret;
        }

        public static void Destroy()
        {
            HttpContext.Current.Session["isLogin"] = 0;
            HttpContext.Current.Session["user"] = null;
        }

    }
}