﻿namespace FitnessDestiny.Services.Blog.Implementations
{
    using AutoMapper.QueryableExtensions;
    using FitnessDestiny.Blog.Services;
    using FitnessDestiny.Data;
    using FitnessDestiny.Data.Models;
    using FitnessDestiny.Services.Blog.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static ServiceConstants;

    public class ArticleService : IArticleService
    {
        private readonly FitnessDestinyDbContext db;

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


        public async Task<IEnumerable<ArticleListingServiceModel>> FindAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                .Articles
                .OrderByDescending(c => c.Id)
                .Where(c => c.Title.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<ArticleListingServiceModel>()
                .ToListAsync();
        }

        public async Task<ArticleDetailsServiceModel> ByIdAsync(int id)
            => await this.db
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<ArticleDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task CreateAsync(string title, string content, string authorId)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.UtcNow,
                AuthorId = authorId
            };

            this.db.Add(article);

            await this.db.SaveChangesAsync();
        }
        
        public async Task<bool> EditAsync(int id, string title, string content)
        {
            var article = await this.db.Articles.Where(a => a.Id == id).FirstOrDefaultAsync();

            if (article == null)
            {
                return false;
            }

            article.Title = title;
            article.Content = content;

            await this.db.SaveChangesAsync();
            return true;
        }

        public bool Delete(int id)
        {
            Article article = this.db.Articles.Find(id);

            if (article == null)
            {
                return false;
            }

            this.db.Articles.Remove(article);
            this.db.SaveChanges();
            return true;
        }


        public async Task<int> TotalAsync()
            => await this.db.Articles.CountAsync();

        public async Task<ArticleEditServiceModel> EditByIdAsync(int id)
        => await this.db
              .Articles
              .Where(a => a.Id == id)
              .ProjectTo<ArticleEditServiceModel>()
              .FirstOrDefaultAsync();


        public async Task AddCommentAsync(int articleId, string comment, string userId)
        {
            var article = await this.db
                .Articles
                .FindAsync(articleId);

            var currentTime = DateTime.UtcNow;

            var articleComment = new ArticleComment
            {
                ArticleId = articleId,
                AuthorId = userId,
                Content = comment,
                PublishDate = currentTime
            };

            article.Comments.Add(articleComment);
            article.LastCommentDate = currentTime;
            await db.SaveChangesAsync();

        }

    }
}
