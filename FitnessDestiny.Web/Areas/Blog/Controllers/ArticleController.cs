namespace FitnessDestiny.Web.Areas.Blog.Controllers
{
    using FitnessDestiny.Blog.Services;
    using FitnessDestiny.Data.Models;
    using FitnessDestiny.Web.Areas.Blog.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using static WebConstants;

    [Area(BlogArea)]
    public class ArticleController : Controller
    {
        private readonly IArticleService articles;
        private readonly UserManager<User> userManager;

        public ArticleController(
            IArticleService articles,
            UserManager<User> userManager)
        {
            this.articles = articles;
            this.userManager = userManager;
        }


        public async Task<IActionResult> Index(int page = 1)
        => View(new ArticleListingViewModel
        {
            Articles = await this.articles.AllAsync(page),
            TotalArticles = await this.articles.TotalAsync(),
            CurrentPage = page
        });
    }
}