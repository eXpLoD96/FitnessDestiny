namespace FitnessDestiny.Services.Implementations
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Text;
    using FitnessDestiny.Services.Models;

    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly ConcurrentDictionary<string, ShoppingCart> carts;

        public ShoppingCartManager()
        {
            this.carts = new ConcurrentDictionary<string, ShoppingCart>();
        }

        public void AddToCart(string id, int supplementId)
        {
            var shoppingCart = this.GetShoppingCart(id);
            shoppingCart.AddToCart(supplementId);
        }

        public void RemoveFromCart(string id, int supplementId)
        {
            var shoppingCart = this.GetShoppingCart(id);
            shoppingCart.RemoveFromCart(supplementId);
        }

        public IEnumerable<CartItem> GetItems(string id)
        {
            var shoppingCart = this.GetShoppingCart(id);

            return new List<CartItem>(shoppingCart.Items);
        }

        private ShoppingCart GetShoppingCart(string id)
        {
            return this.carts.GetOrAdd(id, new ShoppingCart());
        }

        public void Clear(string id) => this.GetShoppingCart(id).Clear(); 
        
    }
}
