namespace FitnessDestiny.Services.Admin.Models
{
    using FitnessDestiny.Core.Mapping;
    using FitnessDestiny.Data.Models;

    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
