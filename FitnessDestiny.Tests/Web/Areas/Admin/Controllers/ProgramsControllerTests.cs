namespace FitnessDestiny.Tests.Web.Areas.Admin.Controllers
{
    using FitnessDestiny.Data.Models;
    using FitnessDestiny.Services.Admin;
    using FitnessDestiny.Tests.Mocks;
    using FitnessDestiny.Web;
    using FitnessDestiny.Web.Areas.Admin.Controllers;
    using FitnessDestiny.Web.Areas.Admin.Models.Programs;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ProgramsControllerTests
    {
        private const string FirstUserId = "1";
        private const string FirstUserUsername = "First";
        private const string SecondUserId = "2";
        private const string SecondUserUsername = "Second";

        [Fact]
        public void ProgramsControllerShouldBeInAdminArea()
        {
            // Arrange
            var controller = typeof(ProgramsController);

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
        public void ProgramsControllerShouldBeOnlyForAdminUsers()
        {
            // Arrange
            var controller = typeof(ProgramsController);

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
        public async Task GetCreateShouldReturnViewWithValidModel()
        {
            // Arrange
            var userManager = this.GetUserManagerMock();
            var controller = new ProgramsController(userManager.Object, null);

            // Act
            var result = await controller.Create();

            // Assert
            result.Should().BeOfType<ViewResult>();

            var model = result.As<ViewResult>().Model;

            model.Should().BeOfType<AddProgramFormModel>();

            var formModel = model.As<AddProgramFormModel>();

            formModel.StartDate.Year.Should().Be(DateTime.UtcNow.Year);
            formModel.StartDate.Month.Should().Be(DateTime.UtcNow.Month);
            formModel.StartDate.Day.Should().Be(DateTime.UtcNow.Day);

            var endDate = DateTime.UtcNow.AddDays(30);

            formModel.EndDate.Year.Should().Be(endDate.Year);
            formModel.EndDate.Month.Should().Be(endDate.Month);
            formModel.EndDate.Day.Should().Be(endDate.Day);

            this.AssertTrainersSelectList(formModel.Trainers);
        }

        [Fact]
        public async Task PostCreateShouldReturnViewWithCorrectModelWhenModelStateIsInvalid()
        {
            // Arrange
            var userManager = this.GetUserManagerMock();
            var controller = new ProgramsController(userManager.Object, null);
            controller.ModelState.AddModelError(string.Empty, "Error");

            // Act
            var result = await controller.Create(new AddProgramFormModel());

            // Assert
            result.Should().BeOfType<ViewResult>();

            var model = result.As<ViewResult>().Model;

            model.Should().BeOfType<AddProgramFormModel>();

            var formModel = model.As<AddProgramFormModel>();

            this.AssertTrainersSelectList(formModel.Trainers);
        }

        [Fact]
        public async Task PostCreateShouldReturnRedirectWithValidModel()
        {
            // Arrange
            const string nameValue = "Name";
            const string descriptionValue = "Description";
            var startDateValue = new DateTime(2000, 12, 10);
            var endDateValue = new DateTime(2000, 12, 15);
            const string trainerIdValue = "1";

            string modelName = null;
            string modelDescription = null;
            int modelDaysPerWeek = 0;
            DateTime modelStartDate = DateTime.UtcNow;
            DateTime modelEndDate = DateTime.UtcNow;
            string modelTrainerId = null;
            string successMessage = null;

            var adminCourseService = new Mock<IAdminProgramsService>();
            adminCourseService
                .Setup(c => c.CreateAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<int>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<string>()))
                .Callback((string name, string description, int daysPerWeek, DateTime startDate, DateTime endDate, string trainerId) =>
                {
                    modelName = name;
                    modelDescription = description;
                    modelDaysPerWeek = daysPerWeek;
                    modelStartDate = startDate;
                    modelEndDate = endDate;
                    modelTrainerId = trainerId;
                })
                .Returns(Task.CompletedTask);

            var tempData = new Mock<ITempDataDictionary>();
            tempData
                .SetupSet(t => t[WebConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
                .Callback((string key, object message) => successMessage = message as string);

            var controller = new ProgramsController(null, adminCourseService.Object);
            controller.TempData = tempData.Object;

            // Act
            var result = await controller.Create(new AddProgramFormModel
            {
                Name = nameValue,
                Description = descriptionValue,
                StartDate = startDateValue,
                EndDate = endDateValue,
                TrainerId = trainerIdValue
            });

            // Assert
            modelName.Should().Be(nameValue);
            modelDescription.Should().Be(descriptionValue);
            modelStartDate.Should().Be(startDateValue);
            modelEndDate.Should().Be(endDateValue.AddDays(1));
            modelTrainerId.Should().Be(trainerIdValue);

            successMessage.Should().Be($"Program {nameValue} created successfully!");

            result.Should().BeOfType<RedirectToActionResult>();

            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
            result.As<RedirectToActionResult>().ControllerName.Should().Be("Home");
            result.As<RedirectToActionResult>().RouteValues.Keys.Should().Contain("area");
            result.As<RedirectToActionResult>().RouteValues.Values.Should().Contain(string.Empty);
        }

        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.GetUsersInRoleAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<User>
                {
                    new User { Id = FirstUserId, UserName = FirstUserUsername },
                    new User { Id = SecondUserId, UserName = SecondUserUsername }
                });

            return userManager;
        }

        private void AssertTrainersSelectList(IEnumerable<SelectListItem> trainers)
        {
            trainers.Should().Match(items => items.Count() == 2);
            trainers.First().Should().Match(u => u.As<SelectListItem>().Value == FirstUserId);
            trainers.First().Should().Match(u => u.As<SelectListItem>().Text == FirstUserUsername);
            trainers.Last().Should().Match(u => u.As<SelectListItem>().Value == SecondUserId);
            trainers.Last().Should().Match(u => u.As<SelectListItem>().Text == SecondUserUsername);
        }
    }

}

