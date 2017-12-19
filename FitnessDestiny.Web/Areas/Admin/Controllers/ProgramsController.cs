using FitnessDestiny.Data.Models;
using FitnessDestiny.Services.Admin;
using FitnessDestiny.Web.Areas.Admin.Models.Programs;
using FitnessDestiny.Web.Controllers;
using FitnessDestiny.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessDestiny.Web.Areas.Admin.Controllers
{
    public class ProgramsController : BaseAdminController
    {
        private readonly UserManager<User> userManager;
        private readonly IAdminProgramsService program;

        public ProgramsController(
            UserManager<User> userManager,
            IAdminProgramsService program)
        {
            this.userManager = userManager;
            this.program = program;
        }

        public async Task<IActionResult> Create()
            => View(new AddProgramFormModel
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                Trainers = await this.GetTrainers()
            });

        [HttpPost]
        public async Task<IActionResult> Create(AddProgramFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Trainers = await this.GetTrainers();
                return View(model);
            }

            await this.program.CreateAsync(
                model.Name,
                model.Description,
                model.DaysPerWeek,
                model.StartDate,
                model.EndDate.AddDays(1),
                model.TrainerId);

            TempData.AddSuccessMessage($"Program {model.Name} created successfully!");

            return RedirectToAction(nameof(HomeController.Index),
                "Home",
                new { area = string.Empty });
        }

        private async Task<IEnumerable<SelectListItem>> GetTrainers()
        {
            var trainers = await this.userManager.GetUsersInRoleAsync(WebConstants.Trainer);

            var trainerListItems = trainers
                .Select(t => new SelectListItem
                {
                    Text = t.UserName,
                    Value = t.Id
                })
                .ToList();

            return trainerListItems;
        }
    }
}
