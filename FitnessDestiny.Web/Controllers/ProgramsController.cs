namespace FitnessDestiny.Web.Controllers
{
    using FitnessDestiny.Data.Models;
    using FitnessDestiny.Services;
    using FitnessDestiny.Services.Models;
    using FitnessDestiny.Web.Infrastructure.Extensions;
    using FitnessDestiny.Web.Models.Programs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProgramsController : Controller
    {
        private readonly IProgramService programs;
        private readonly UserManager<User> userManager;

        public ProgramsController(
            IProgramService programs,
            UserManager<User> userManager)
        {
            this.programs = programs;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
         => View(new ProgramListingViewModel
         {
             Programs = await this.programs.AllAsync(page),
             TotalPrograms = await this.programs.TotalAsync(),
             CurrentPage = page
         });

        public async Task<IActionResult> Details(int id)
        {
            var model = new ProgramDetailsViewModel
            {
                Program = await this.programs.ByIdAsync<ProgramDetailsServiceModel>(id)
            };

            if (model.Program == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(User);

                model.UserIsEnrolledProgram =
                    await this.programs.UserIsEnrolledProgramAsync(id, userId);
            }

            return View(model);
        }

        [Authorize(Roles = WebConstants.Administrator + "," + WebConstants.VipUser)]
        [HttpPost]
        public async Task<IActionResult> SignUp(int id)
        {
            if(!User.IsInRole(WebConstants.VipUser) || !User.IsInRole(WebConstants.Administrator))
            {
                TempData.AddErrorMessage($"You need to be a VIP user to sign up to a program!");
            }

            var userId = this.userManager.GetUserId(User);

            var success = await this.programs.SignUpUserAsync(id, userId);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Thank you for your registration!");

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize(Roles = WebConstants.Administrator + "," + WebConstants.VipUser)]
        [HttpPost]
        public async Task<IActionResult> SignOut(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var success = await this.programs.SignOutUserAsync(id, userId);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Sorry to see you go!");

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
