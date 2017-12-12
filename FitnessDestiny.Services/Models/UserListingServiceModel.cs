namespace FitnessDestiny.Services.Models
{
    using AutoMapper;
    using FitnessDestiny.Core.Mapping;
    using FitnessDestiny.Data.Models;

    public class UserListingServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public int Programs { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<User, UserListingServiceModel>()
                .ForMember(u => u.Programs, cfg => cfg.MapFrom(u => u.ProgramsEnrolled.Count));
    }
}
