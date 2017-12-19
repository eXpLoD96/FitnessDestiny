namespace FitnessDestiny.Tests.Web
{
    using Data.Models;
    using FluentAssertions;
    using FitnessDestiny.Services;
    using FitnessDestiny.Services.Models;
    using FitnessDestiny.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Moq;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class UsersControllerTests
    {
        [Fact]
        public async Task ProfileShouldReturnNotFoundWithInvalidUsername()
        {
            // Arrange
            var userManager = UserManagerMock.New;

            var controller = new UsersController(null, userManager.Object);

            // Act
            var result = await controller.Profile("Username");

            // Assert
            result
                .Should()
                .BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task ProfileShouldReturnViewWithCorrectModelWithValidUsername()
        {
            // Arrange
            const string userId = "SomeId";
            const string username = "SomeUsername";

            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new User { Id = userId });

            var userService = new Mock<IUserService>();
            userService
                .Setup(u => u.ProfileAsync(It.Is<string>(id => id == userId)))
                .ReturnsAsync(new UserProfileServiceModel { UserName = username });

            var controller = new UsersController(
                userService.Object, userManager.Object);

            // Act
            var result = await controller.Profile(username);

            // Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<UserProfileServiceModel>().UserName == username);
        }


    }
}
