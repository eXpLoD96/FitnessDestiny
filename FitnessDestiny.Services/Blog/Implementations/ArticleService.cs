namespace FitnessDestiny.Services.Blog.Implementations
{
    using AutoMapper.QueryableExtensions;
    using FitnessDestiny.Blog.Services;
    using FitnessDestiny.Data;
    using FitnessDestiny.Services.Blog.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static ServiceConstants;

    public class ArticleService : IArticleService
    {
        public readonly FitnessDestinyDbContext db;

        public ArticleService(FitnessDestinyDbContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<ArticleListingServiceModel>> AllAsync(int page = 1)
        => await this.db
                .Articles
                .OrderByDescending(a => a.PublishDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ProjectTo<ArticleListingServiceModel>()
                .ToListAsync();

        public async Task<int> TotalAsync()
            => await this.db.Articles.CountAsync();
    }
}
