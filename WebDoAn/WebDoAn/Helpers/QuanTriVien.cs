using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDoAn.Models;

namespace WebDoAn.Helpers
{    
    public class QuanTriVien
    {
        public List<User> ItemsUsers { get; set; }
        public QuanTriVien()
        {
            this.ItemsUsers = new List<User>();        
        }

        //public void RemoveItemsUsers(int proId)
        //{
        //    using (var ctx = new QLBHEntities())
        //    {
        //        this.ItemsUsers = ctx.Users
        //                      .ToList();
        //    }

        //    var toDeleteItem = this.ItemsUsers
        //       .Where(i => i.f_ID == proId)
        //       .FirstOrDefault();
        //    if (toDeleteItem != null)
        //    {
        //        this.ItemsUsers.Remove(toDeleteItem);
        //    }
        //}
    }
}