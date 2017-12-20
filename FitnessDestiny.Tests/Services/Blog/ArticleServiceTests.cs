using AutoMapper;
using FitnessDestiny.Data;
using FitnessDestiny.Data.Models;
using FitnessDestiny.Services.Blog.Implementations;
using FitnessDestiny.Services.Blog.Models;
using FitnessDestiny.Web.Infrastructure.Mapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FitnessDestiny.Tests.Services.Blog
{
    public class ArticleServiceTests
    {
        

        [Fact]
        public async Task FindByIdAsyncShouldReturnCorrectArticle()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstArticle = new Article { Id = 14, Title = "First article", Content = new String('p', 255) };
            var secondArticle = new Article { Id = 25, Title = "Second article", Content = new String('a', 255) };
            db.AddRange(firstArticle, secondArticle);
            await db.SaveChangesAsync();

            var articleService = new ArticleService(db);

            // Act
            var result = await articleService.ByIdAsync(secondArticle.Id);

            // Assert 
            result
                .Id
                .Should()
                .Be(25);
        }

        [Fact]
        public async Task FindAsyncShouldReturnCorrectResultWithFilterAndOrder()
        {
            // Arrange
            var db = this.GetDatabase();
            var firstArticle = new Article { Id = 1, Title = "First article", Content = new String('p', 255)};
            var secondArticle = new Article { Id = 2, Title = "Second article", Content = new String('a', 255)};
            

            db.AddRange(firstArticle, secondArticle);

            await db.SaveChangesAsync();

            var articleService = new ArticleService(db);

            // Act
            var result = await articleService.FindAsync("article");

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 2
                    && r.ElementAt(1).Id == 1)
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task EditShouldReturnCorrectServiceModel()
        {
            // Arrange
            var db = this.GetDatabase();
            var firstArticle = new Article { Id = 1, Title = "First article", Content = new String('p', 255) };
            db.Add(firstArticle);

            await db.SaveChangesAsync();
            var articleService = new ArticleService(db);

            // Act
            var result = await articleService.EditByIdAsync(firstArticle.Id);

            // Assert
            result
                .Should()
                .BeOfType<ArticleEditServiceModel>();
        }

        [Fact]
        public async Task AddCommentShouldIncreaseCommentCount()
        {
            // Arrange
            var db = this.GetDatabase();
            var user = new User { Id = "admin", Email = "admin@admin.bg" };
            var firstArticle = new Article { Id = 1, Title = "First article", Content = new String('p', 255) };
            var articleComment = new ArticleComment { ArticleId = 1, Content = "Asd", Author = user, AuthorId = "admin" };

            await db.Articles.AddAsync(firstArticle);
            await db.Comments.AddAsync(articleComment);

            await db.SaveChangesAsync();
            var articleService = new ArticleService(db);

            // Act
            var result = articleService.AddCommentAsync(firstArticle.Id, articleComment.Content, user.Id);

            // Assert
            firstArticle
                .Comments
                .Count
                .Should()
                .Be(2);

        }
            private FitnessDestinyDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<FitnessDestinyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new FitnessDestinyDbContext(dbOptions);
        }
    }
}
