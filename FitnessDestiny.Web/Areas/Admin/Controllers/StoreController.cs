using FitnessDestiny.Services.Admin;
using FitnessDestiny.Services.Html;
using FitnessDestiny.Web.Areas.Admin.Models.Store;
using FitnessDestiny.Web.Infrastructure.Filters;
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
        private readonly IHtmlService html;

        public StoreController(
            IAdminStoreService store,
            IHtmlService html)
        {
            this.store = store;
            this.html = html;
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(CreateSupplementFormModel model)
        {
            model.Description = this.html.Sanitize(model.Description);
            
            await this.store.CreateAsync(model.Name, model.Description, model.Brand, model.ImageUrl, model.SupplementType, model.Price, model.Quantity);

            return RedirectToRoute("");
        }

        public async Task<IActionResult> Delete(int id)
        {

        }
    }
}
