using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDoAn.Models;

namespace WebDoAn.Helpers
{
    public class ListPro
    {
        public List<Product> ItemsListPro { get; set; }

        public ListPro()
        {
            this.ItemsListPro = new List<Product>();        
        }
    }
}