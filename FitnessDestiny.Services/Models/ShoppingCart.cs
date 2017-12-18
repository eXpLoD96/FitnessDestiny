namespace FitnessDestiny.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ShoppingCart
    {
        private readonly IList<CartItem> items;

        public ShoppingCart()
        {
            this.items = new List<CartItem>();
        }

        public IEnumerable<CartItem> Items => new List<CartItem>(this.items);

        public void AddToCart(int supplementId)
        {
            var cartItem = this.items.FirstOrDefault(s => s.SupplementId == supplementId);
            if(cartItem == null)
            {
                cartItem = new CartItem
                {
                    SupplementId = supplementId,
                    Quantity = 1
                };

                this.items.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
        }

        public void RemoveFromCart(int supplementId)
        {
            var cartItem = this.items.FirstOrDefault(i => i.SupplementId == supplementId);
            if(cartItem != null)
            {
                this.items.Remove(cartItem);
            }
        }

        public void Clear()
        {
            this.items.Clear();
        }
    }
}
