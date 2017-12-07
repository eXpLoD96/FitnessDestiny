namespace FitnessDestiny.Blog.Services
{
    using FitnessDestiny.Services.Blog.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleService
    {
        Task<IEnumerable<ArticleListingServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();
    }
}
