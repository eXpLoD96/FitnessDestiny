namespace FitnessDestiny.Services.Models
{
    using AutoMapper;
    using FitnessDestiny.Core.Mapping;
    using FitnessDestiny.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProgramListingServiceModel : IMapFrom<Program>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public User Trainer { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Program, ProgramListingServiceModel>()
                .ForMember(c => c.Trainer, cfg => cfg.MapFrom(c => c.Trainer.UserName));
    }
}
