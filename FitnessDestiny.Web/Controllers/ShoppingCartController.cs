using FitnessDestiny.Data;
using FitnessDestiny.Data.Models;
using FitnessDestiny.Services;
using FitnessDestiny.Services.Models;
using FitnessDestiny.Web.Infrastructure.Extensions;
using FitnessDestiny.Web.Models.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessDestiny.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartManager shoppingCartManager;
        private readonly FitnessDestinyDbContext db;
        private readonly UserManager<User> users;

        public ShoppingCartController(
            IShoppingCartManager shoppingCartManager,
            FitnessDestinyDbContext db,
            UserManager<User> users)
        {
            this.shoppingCartManager = shoppingCartManager;
            this.db = db;
            this.users = users;
        }

        public IActionResult Items()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.shoppingCartManager.GetItems(shoppingCartId);

            var itemIds = items.Select(i => i.SupplementId);

            var itemsWithDetails = this.GetCartItems(items);

            return View(itemsWithDetails);
        }

        public IActionResult AddToCart(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            this.shoppingCartManager.AddToCart(shoppingCartId, id);

            return RedirectToAction(nameof(Items));
        }

        [Authorize]
        public IActionResult FinishOrder()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.shoppingCartManager.GetItems(shoppingCartId);

            var itemsWithDetails = this.GetCartItems(items);

            var order = new Order
            {
                UserId = this.users.GetUserId(User),
                TotalPrice = itemsWithDetails.Sum(i => i.Price * i.Quantity)
            };

            foreach (var item in itemsWithDetails)
            {
                order.Items.Add(new OrderItem
                {
                    SupplementId = item.SupplementId,
                    SupplementPrice = item.Price,
                    Quantity = item.Quantity
                });
            }

            this.db.Add(order);
            this.db.SaveChanges();

            shoppingCartManager.Clear(shoppingCartId);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        private List<CartItemViewModel> GetCartItems(IEnumerable<CartItem> items)
        {
            var itemIds = items.Select(s => s.SupplementId);
            var itemsWithDetails = this.db
                    .Supplements
                    .Where(sup => itemIds.Contains(sup.Id))
                    .Select(sup => new CartItemViewModel
                    {
                        SupplementId = sup.Id,
                        Name = sup.Name,
                        Price = sup.Price
                    })
                .ToList();


            var itemQuantities = items.ToDictionary(i => i.SupplementId, i => i.Quantity);
            // fix automapper

            itemsWithDetails.ForEach(i => i.Quantity = itemQuantities[i.SupplementId]);

            return itemsWithDetails;
        }
    }

}

