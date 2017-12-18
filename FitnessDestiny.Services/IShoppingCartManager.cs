namespace FitnessDestiny.Services
{
    using FitnessDestiny.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IShoppingCartManager
    {
        void AddToCart(string id, int supplementId);

        void RemoveFromCart(string id, int supplementId);

        IEnumerable<CartItem> GetItems(string id);

        void Clear(string id);
    }
}
