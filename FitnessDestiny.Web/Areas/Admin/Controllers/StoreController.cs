using FitnessDestiny.Data.Models;
using FitnessDestiny.Services.Admin;
using FitnessDestiny.Services.Admin.Models.Store;
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

        public async Task<IActionResult> Edit(int id)
        {
            var supplement = await this.store.EditByIdAsync(id);

            return this.View(new SupplementEditServiceModel
            {
                Id = supplement.Id,
                Name = supplement.Name,
                Description = supplement.Description,
                Brand = supplement.Brand,
                ImageUrl = supplement.ImageUrl,
                SupplementType = supplement.SupplementType,
                Price = supplement.Price,
                Quantity = supplement.Quantity,
                InStock = supplement.InStock
            });
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> EditSupplement([FromForm]SupplementEditServiceModel parameter)
        {
            parameter.Name = this.html.Sanitize(parameter.Name);
            parameter.Description = this.html.Sanitize(parameter.Description);
            parameter.Brand = this.html.Sanitize(parameter.Brand);
            parameter.ImageUrl = this.html.Sanitize(parameter.ImageUrl);

            await this.store.EditAsync(parameter.Id, parameter.Name, parameter.Description,
                parameter.Brand, parameter.ImageUrl, parameter.SupplementType, parameter.Price,
                parameter.Quantity, parameter.InStock);

            return RedirectToAction(nameof(Index));
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
