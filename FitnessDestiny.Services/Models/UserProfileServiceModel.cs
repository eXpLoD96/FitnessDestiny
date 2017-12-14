namespace FitnessDestiny.Services.Models
{
    using AutoMapper;
    using FitnessDestiny.Core.Mapping;
    using FitnessDestiny.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserProfileServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public IEnumerable<UserProfileProgramServiceModel> Programs { get; set; }

        public IEnumerable<UserProfileArticlesServiceModel> Articles { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<User, UserProfileServiceModel>()
                .ForMember(up => up.Articles, cfg => cfg.MapFrom(u => u.Articles))
                .ForMember(u => u.Programs, cfg => cfg.MapFrom(s => s.ProgramsEnrolled.Select(p => p.Program)));
        }
    }
}
