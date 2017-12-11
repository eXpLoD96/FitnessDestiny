namespace FitnessDestiny.Web.Areas.Store.Controllers
{
    using FitnessDestiny.Data;
    using FitnessDestiny.Services.Store;
    using FitnessDestiny.Web.Areas.Store.Models;
    using FitnessDestiny.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using static FitnessDestiny.Web.WebConstants;

    [Area(StoreArea)]
    public class StoreController : Controller
    {
        public readonly IStoreService store;
        public readonly FitnessDestinyDbContext db;

        public StoreController(IStoreService store, FitnessDestinyDbContext db)
        {
            this.store = store;
            this.db = db;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        => View(new SupplementListingViewModel
        {
            Supplements = await this.store.AllAsync(page),
            TotalSupplements = await this.store.TotalAsync(),
            CurrentPage = page
        });

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
            => this.ViewOrNotFound(await this.store.ByIdAsync(id));


    }
}
