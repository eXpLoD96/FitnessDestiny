using FitnessDestiny.Services.Admin;
using FitnessDestiny.Services.Html;
using FitnessDestiny.Services.Store;
using FitnessDestiny.Web.Areas.Admin.Models.Store;
using FitnessDestiny.Web.Areas.Store.Models;
using FitnessDestiny.Web.Infrastructure.Extensions;
using FitnessDestiny.Web.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessDestiny.Web.Areas.Admin.Controllers
{
    public class StoreController : BaseAdminController
    {
        private readonly IAdminStoreService store;
        private readonly IStoreService userStore;
        private readonly IHtmlService html;

        public StoreController(
            IAdminStoreService store,
            IStoreService userStore,
            IHtmlService html)
        {
            this.store = store;
            this.userStore = userStore;
            this.html = html;
        }

        [Authorize]
        public async Task<IActionResult> Index(int page = 1)
        => View(new SupplementListingViewModel
        {
            Supplements = await this.userStore.AllAsync(page),
            TotalSupplements = await this.userStore.TotalAsync(),
            CurrentPage = page
        });

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(CreateSupplementFormModel model)
        {
            model.Description = this.html.Sanitize(model.Description);
            
            await this.store.CreateAsync(model.Name, model.Description, model.Brand, model.ImageUrl, model.SupplementType, model.Price, model.Quantity);

            return RedirectToRoute("");
        }

        public IActionResult Delete(int id)
            => this.View(id);

        [HttpPost]
        public IActionResult Destroy(int id)
        {
            if (!this.store.Delete(id))
            {
                return this.NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
