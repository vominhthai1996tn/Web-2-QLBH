using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDoAn.Models
{
    public class AddProductVM
    {
        public int ProID { get; set; }
        public string ProName { get; set; }
        public string TyneDes { get; set; }
        public string FullDes { get; set; }
        public decimal Price { get; set; }
        public string CatID { get; set; }
        public string DateTime { get; set; }
        public int IDSell { get; set; }
    }
}