namespace FitnessDestiny.Web.Areas.Blog.Controllers
{
    using FitnessDestiny.Blog.Services;
    using FitnessDestiny.Data.Models;
    using FitnessDestiny.Services.Blog.Models;
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
    [Authorize(Roles = BlogAuthor + "," + Administrator)]
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

        [AllowAnonymous]
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
            => this.ViewOrNotFound(await this.articles.ByIdAsync(id));

        //[Authorize(Roles = Administrator)]
        //public IActionResult Edit(ArticleEditServiceModel model) => View(model);

        public async Task<IActionResult> Edit(int id)
        {
            var article = await this.articles.EditByIdAsync(id);
            //return this.View(await this.articles.EditById(id));
            return this.View(new ArticleEditServiceModel
            {
                Title = article.Title,
                Content = article.Content
            });
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> EditArticle([FromForm]ArticleEditServiceModel parameter)
        {
            parameter.Title = this.html.Sanitize(parameter.Title);
            parameter.Content = this.html.Sanitize(parameter.Content);

            //var user = this.userManager.GetUserName(User.Identity.Name)
            
            await this.articles.EditAsync(parameter.Id, parameter.Title, parameter.Content);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id) => View(id);
        
        [HttpPost]
        public IActionResult Destroy(int id)
        {
            if (!this.articles.Delete(id))
            {
                return this.NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Comment(int id, string comment)
        {
            var article = await this.articles.ByIdAsync(id);
            if (article == null)
            {
                return BadRequest();
            }

            if (comment.Length < 5 || comment.Length > 200)
            {
                TempData.AddErrorMessage(CommentTextLengthErrorText);
                return RedirectToAction(nameof(Details), new { id = id });
            }

            var userId = this.userManager.GetUserId(User);
            await this.articles.AddCommentAsync(id, comment, userId);

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}