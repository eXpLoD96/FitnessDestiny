namespace FitnessDestiny.Web.Areas.Admin.Models.Users
{
    using FitnessDestiny.Services.Admin.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class UserListingsViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
