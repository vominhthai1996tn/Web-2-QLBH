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

         public void ListOrder(int userid, int[] a)
         {
            
             using (var ctx = new QLBHEntities())
             {
                 var lOrder = ctx.Orders.Where(i => i.UserID == userid).ToList();
                     

                
             }     
         }
    }
}