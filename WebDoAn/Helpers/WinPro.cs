using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDoAn.Models;

namespace WebDoAn.Helpers
{
    public class WinPro
    {
        public List<OrderDetail> ItemsOrderDetail { get; set; }

         public WinPro()
        {
            this.ItemsOrderDetail = new List<OrderDetail>();        
        }

         public bool checkIDOrder(int idorder, int iduser)
         {
             using (var ctx = new QLBHEntities())
             {
                 var list = ctx.Orders
                     .Where(od => od.OrderID == idorder && od.UserID == iduser)                   
                     .FirstOrDefault();

                 if (list == null)
                     return false;
             }

             return true;
         }

         public string OutNameProduct(int idpro)
         {
             using (var ctx = new QLBHEntities())
             {
                 var pro = ctx.Products
                     .Where(p => p.ProID == idpro)
                     .FirstOrDefault();

                 if (pro != null)
                     return pro.ProName;
                 else
                     return null;
             }
         }
    }
}
