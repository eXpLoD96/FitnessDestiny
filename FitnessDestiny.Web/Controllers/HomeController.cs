namespace FitnessDestiny.Web.Controllers
{
    using FitnessDestiny.Blog.Services;
    using FitnessDestiny.Services.Store;
    using FitnessDestiny.Web.Models;
    using FitnessDestiny.Web.Models.Home;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IStoreService supplements;
        private readonly IArticleService articles;

        public HomeController(
            IStoreService supplements,
            IArticleService articles)
        {
            this.supplements = supplements;
            this.articles = articles;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Search(SearchFormModel model)
        {
            var viewModel = new SearchViewModel
            {
                SearchText = model.SearchText
            };

            if (model.SearchInArticles)
            {
                viewModel.Articles = await this.articles.FindAsync(model.SearchText);
            }

            if (model.SearchInSupplements)
            {
                viewModel.Supplements = await this.supplements.FindAsync(model.SearchText);
            }

            return View(viewModel);
        }
    }
}
