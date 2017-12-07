namespace FitnessDestiny.Web.Areas.Blog.Controllers
{
    using FitnessDestiny.Blog.Services;
    using FitnessDestiny.Data.Models;
    using FitnessDestiny.Services.Html;
    using FitnessDestiny.Web.Areas.Blog.Models;
    using FitnessDestiny.Web.Infrastructure.Extensions;
    using FitnessDestiny.Web.Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using static WebConstants;

    [Area(BlogArea)]
    public class ArticleController : Controller
    {
        private readonly IArticleService articles;
        private readonly UserManager<User> userManager;
        private readonly IHtmlService html;

        public ArticleController(
            IArticleService articles,
            UserManager<User> userManager,
            IHtmlService html)
        {
            this.articles = articles;
            this.userManager = userManager;
            this.html = html;
        }


        public async Task<IActionResult> Index(int page = 1)
        => View(new ArticleListingViewModel
        {
            Articles = await this.articles.AllAsync(page),
            TotalArticles = await this.articles.TotalAsync(),
            CurrentPage = page
        });

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(PublishArticleFormModel model)
        {
            model.Content = this.html.Sanitize(model.Content);

            var userId = this.userManager.GetUserId(User);

            await this.articles.CreateAsync(model.Title, model.Content, userId);

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
            => this.ViewOrNotFound(await this.articles.ById(id));


    }
}