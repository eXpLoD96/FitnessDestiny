namespace FitnessDestiny.Tests.Web.Areas.Admin.Controllers
{
    using FitnessDestiny.Services.Store;
    using FitnessDestiny.Services.Store.Models;
    using FitnessDestiny.Web;
    using FitnessDestiny.Web.Areas.Admin.Controllers;
    using FitnessDestiny.Web.Areas.Admin.Models.Store;
    using FitnessDestiny.Web.Areas.Store.Models;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class StoreControllerTests
    {
        [Fact]
        public void StoreControllerShouldBeInAdminArea()
        {
            // Arrange
            var controller = typeof(StoreController);

            // Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AreaAttribute))
                as AreaAttribute;

            // Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.RouteValue.Should().Be(WebConstants.AdminArea);
        }

        [Fact]
        public void StoreControllerShouldBeOnlyForAdminUsers()
        {
            // Arrange
            var controller = typeof(StoreController);

            // Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            // Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.Roles.Should().Be(WebConstants.Administrator);
        }

        [Fact]
        public void GetCreateShouldReturnViewWithValidModel()
        {
            // Arrange
            var controller = new StoreController(null, null, null);

            // Act
            var result = controller.Create();

            // Assert
            result.Should().BeOfType<ViewResult>();
            
        }
    }
}
