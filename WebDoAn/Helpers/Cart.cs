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

        public int SumQ(int idUser)
        {
            return this.Items.Where(i => i.idUser == idUser).Sum(i => i.Quantity);
        }
        public void AddItem(CartItem item)
        {
            var eItem = this.Items
                .Where(i => i.Product.ProID == item.Product.ProID)
                .FirstOrDefault();

            if (eItem != null)
            {
                //eItem.Quantity += item.Quantity;
                if (item.idUser != eItem.idUser && item.Price > eItem.Price)//
                {
                    this.Items.Add(item);
                }
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


        //public void UpdateItem(int proId, int proPirce)
        //{

        //    var toUpdateItem = this.Items
        //       .Where(i => i.Product.ProID == proId)
        //       .FirstOrDefault();
        //    if (toUpdateItem != null && toUpdateItem.idUser == CurrentContext.GetCurUser().f_ID)
        //    {
        //        toUpdateItem.Price = proPirce;
        //    }
        //}

        //kiem tra gia dau co hop le khong
        public bool IsPrice(int proId, int proPirce)
        {
            var eItem = this.Items
            .Where(i => i.Product.ProID == proId)
            .FirstOrDefault();

            if (eItem != null) {
                if (proPirce <= eItem.Price)
                    return false;
            }          

            return true;
        }

        // kiem tra gia de thong bao cho nguoi dung biet co nguoi ra gia cao hon
        public bool IsPriceUser(int idUser, int proId, int proPirce)
        {
            var eItem = this.Items
            .Where(i => i.idUser != idUser)
            .FirstOrDefault();

            if (eItem != null)
            {
                if (proId == eItem.Product.ProID && proPirce <= eItem.Price)
                        return false;
                
            }

            return true;
        }

        //kiem tra nguoi dùng idUser, dau giá sản phẩm idPro có đưa ra giá đấu cao nhất hay không
        public bool PriceMax(int idUser, int idPro)
        {            
            var uwin = this.Items
                .Where(i => i.idUser == idUser && i.Product.ProID == idPro)
                .FirstOrDefault();

            //nếu chỉ có duy nhất 1 người đấu gia thì người dùng này thắng
            var temp = this.Items.Where(i => i.Product.ProID == idPro && i.idUser != idUser).FirstOrDefault();
            if (temp == null)
                return true;

            //nhiều người cùng đấu giá
            foreach (var user in this.Items.Where(i => i.Product.ProID == idPro && i.idUser != idUser))
            {
                if (uwin.moneyUser <= user.moneyUser)
                {
                    return false;
                }
            }

            return true;
        }

    }
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int idUser { get; set; }
        public decimal moneyUser { get; set; }
    }

   
}