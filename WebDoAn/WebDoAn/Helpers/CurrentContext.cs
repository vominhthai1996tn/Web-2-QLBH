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
        public static ToLike GetToLike()
        {
            var ret = (ToLike)HttpContext.Current.Session["tolike"];
            if (ret == null)
            {
                ret = new ToLike();
                HttpContext.Current.Session["tolike"] = ret;               
            }
                                  
            return ret;
        }

        public static QuanTriVien GetQuanTriVien()
        {
            var ret = (QuanTriVien)HttpContext.Current.Session["quantrivien"];
            if (ret == null)
            {
                ret = new QuanTriVien();
                HttpContext.Current.Session["quantrivien"] = ret;
            }

            return ret;
        }

        public static ListPro GetListPro()
        {
            var ret = (ListPro)HttpContext.Current.Session["listpro"];
            if (ret == null)
            {
                ret = new ListPro();
                HttpContext.Current.Session["listpro"] = ret;
            }

            return ret;
        }

        public static WinPro GetWinPro()
        {
            var ret = (WinPro)HttpContext.Current.Session["winpro"];
            if (ret == null)
            {
                ret = new WinPro();
                HttpContext.Current.Session["winpro"] = ret;
            }

            return ret;
        }

        public static Cat GetCategory()
        {
            var ret = (Cat)HttpContext.Current.Session["cat"];
            if (ret == null)
            {
                ret = new Cat();
                HttpContext.Current.Session["cat"] = ret;
            }

            return ret;
        }

        public static TopMaxPrice GetMaxPrice()
        {
            var ret = (TopMaxPrice)HttpContext.Current.Session["maxprice"];
            if (ret == null)
            {
                ret = new TopMaxPrice();
                HttpContext.Current.Session["maxprice"] = ret;
            }

            return ret;
        }

        public static void Destroy()
        {
            HttpContext.Current.Session["isLogin"] = 0;
            HttpContext.Current.Session["user"] = null;
        }

        //loi
        public static void moneyUser(CartItem item, int id)
        {           
            using (QLBHEntities ctx = new QLBHEntities())
            {
                var user = ctx.Users
                    .Where(u => u.f_ID == id)
                    .FirstOrDefault();
                        
                if (user != null)
                {
                    item.moneyUser = user.f_Money;
                }
            }                                                                                
        }

        public static bool isAdmin(int id)
        {
            using (QLBHEntities ctx = new QLBHEntities())
            {
                var user = ctx.Users
                    .Where(u => u.f_ID == id)
                    .FirstOrDefault();

                if (user.f_Permission == 0)
                {
                    return false;
                }
            }        
            return true;
        }
    }
}