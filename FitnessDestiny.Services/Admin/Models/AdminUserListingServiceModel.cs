using FitnessDestiny.Core.Mapping;
using FitnessDestiny.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessDestiny.Services.Admin.Models
{
    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
