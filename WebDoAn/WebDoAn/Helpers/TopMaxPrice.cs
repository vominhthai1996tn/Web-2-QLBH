using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDoAn.Models;

namespace WebDoAn.Helpers
{
    public class TopMaxPrice
    {
        public List<Product>ProMax { get; set; }
        public TopMaxPrice()
        {
            this.ProMax = new List<Product>();
        }
    }
}