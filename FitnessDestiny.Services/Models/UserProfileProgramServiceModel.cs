namespace FitnessDestiny.Services.Models
{
    using AutoMapper;
    using FitnessDestiny.Core.Mapping;
    using FitnessDestiny.Data.Models;


    public class UserProfileProgramServiceModel : IMapFrom<Program>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Trainer { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Program, UserProfileProgramServiceModel>()
                .ForMember(up => up.Trainer, cfg => cfg
                    .MapFrom(p => p.Trainer.UserName));
        }
    }

}
