using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDoAn.Models;

namespace WebDoAn.Helpers
{
    public class ToLike
    {
        public List<LikeItem> ItemsLike { get; set; }

        public ToLike()
        {
            this.ItemsLike = new List<LikeItem>();
        }

        public int SumLike(int idUser)
        {
            return this.ItemsLike.Where(i => i.idUser == idUser).Sum(i => i.Quantity);
        }

        public void AddLikeItem(LikeItem item)
        {
            var eItem = this.ItemsLike
                .Where(i => i.Product.ProID == item.Product.ProID)
                .FirstOrDefault();

            if (eItem != null)
            {
                if (item.idUser != eItem.idUser)
                {
                    this.ItemsLike.Add(item);
                }
            }
            else
            {
                this.ItemsLike.Add(item);
            }
        }
        public void RemoveLikeItem(int proId)
        {

            var toDeleteItem = this.ItemsLike
               .Where(i => i.Product.ProID == proId)
               .FirstOrDefault();
            if (toDeleteItem != null)
            {
                this.ItemsLike.Remove(toDeleteItem);
            }
        }
    }

    public class LikeItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int idUser { get; set; }
    }
}