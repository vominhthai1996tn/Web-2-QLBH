using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDoAn.Models;

namespace WebDoAn.Helpers
{
    public class Cat
    {
        public List<Category> ItemsCat { get; set; }

        public Cat()
        {
            this.ItemsCat = new List<Category>();
        }

        public int soLuongSP(int catID)
        {           
            using (var ctx = new QLBHEntities())
            {
                var list = ctx.Products.Where(i => i.CatID == catID).ToList();

                return list.Count;
            }           
        }   
    }
}