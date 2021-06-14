using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new();
        public ShoppingCart()
        {

        }
        public ShoppingCart(string userName)
        {

        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach(var item in ShoppingCartItems)
                {
                    totalprice = totalprice + item.Quantity * item.Price;
                }
                return totalprice;
            }

            
        }

    }
}
