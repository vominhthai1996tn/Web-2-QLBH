using System.Collections.Generic;
using WebDoAn.Models;
using System.Linq;

namespace WebDoAn.Helpers
{
    public class Cart
    {
        public List<CartItem> Items { get; set; }

        public Cart()
        {
            this.Items = new List<CartItem>();
        }

        public int SumQ()
        {
            return this.Items.Sum(i => i.Quantity);
        }
        public void AddItem(CartItem item)
        {
            var eItem = this.Items
                .Where(i => i.Product.ProID == item.Product.ProID)
                .FirstOrDefault();

            if (eItem != null)
            {
                eItem.Quantity += item.Quantity;
            }
            else
            {
                this.Items.Add(item);
            }
        }

        public void RemoveItem(int proId)
        {

            var toDeleteItem = this.Items
               .Where(i => i.Product.ProID == proId)
               .FirstOrDefault();
            if (toDeleteItem != null)
            {
                this.Items.Remove(toDeleteItem);
            }
        }
        public void UpdateItem(int proId, int quantity)
        {

            var toUpdateItem = this.Items
               .Where(i => i.Product.ProID == proId)
               .FirstOrDefault();
            if (toUpdateItem != null)
            {
                toUpdateItem.Quantity = quantity;
            }
        }
    }
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}