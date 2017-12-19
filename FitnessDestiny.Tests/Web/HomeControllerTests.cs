namespace FitnessDestiny.Tests.Web
{
    using FitnessDestiny.Services;
    using FitnessDestiny.Services.Models;
    using FitnessDestiny.Services.Store;
    using FitnessDestiny.Services.Store.Models;
    using FitnessDestiny.Web.Controllers;
    using FitnessDestiny.Web.Models.Home;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class HomeControllerTests
    {
        [Fact]
        public async Task SearchShouldReturnNoResultsWithNoCriteria()
        {
            // Arrange
            var controller = new HomeController(null, null, null);

            // Act
            var result = await controller.Search(new SearchFormModel
            {
                SearchInSupplements = false,
                SearchInArticles = false,
                SearchInUsers = false
            });

            // Assert
            result.Should().BeOfType<ViewResult>();

            result.As<ViewResult>().Model.Should().BeOfType<SearchViewModel>();

            var searchViewModel = result.As<ViewResult>().Model.As<SearchViewModel>();

            searchViewModel.Supplements.Should().BeEmpty();
            searchViewModel.Articles.Should().BeEmpty();
            searchViewModel.Users.Should().BeEmpty();
            searchViewModel.SearchText.Should().BeNull();
        }

        [Fact]
        public async Task SearchShouldReturnViewWithValidModelWhenSupplementsAreFiltered()
        {
            // Arrange
            const string searchText = "A";

            var storeService = new Mock<IStoreService>();
            storeService
                .Setup(c => c.FindAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<SupplementListingServiceModel>
                {
                    new SupplementListingServiceModel { Id = 10 }
                });

            var controller = new HomeController(storeService.Object, null, null);

            // Act
            var result = await controller.Search(new SearchFormModel
            {
                SearchText = searchText,
                SearchInSupplements = true,
                SearchInArticles = false,
                SearchInUsers = false
            });

            // Assert
            result.Should().BeOfType<ViewResult>();

            result.As<ViewResult>().Model.Should().BeOfType<SearchViewModel>();

            var searchViewModel = result.As<ViewResult>().Model.As<SearchViewModel>();

            searchViewModel.Supplements.Should().Match(c => c.As<List<SupplementListingServiceModel>>().Count == 1);
            searchViewModel.Supplements.First().Should().Match(c => c.As<SupplementListingServiceModel>().Id == 10);
            searchViewModel.Articles.Should().BeEmpty();
            searchViewModel.Users.Should().BeEmpty();
            searchViewModel.SearchText.Should().Be(searchText);
        }
    }
}
