namespace FitnessDestiny.Blog.Services
{
    using FitnessDestiny.Services.Blog.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleService
    {
        Task<IEnumerable<ArticleListingServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();

        Task<ArticleDetailsServiceModel> ById(int id);

        Task<ArticleEditServiceModel> EditById(int id);

        Task CreateAsync(string title, string content, string authorId);

        Task<bool> EditAsync(int id, string title, string content);

        bool Delete(int id);
    }
}
