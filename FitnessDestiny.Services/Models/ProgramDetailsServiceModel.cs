namespace FitnessDestiny.Services.Models
{
    using AutoMapper;
    using FitnessDestiny.Core.Mapping;
    using FitnessDestiny.Data.Models;
    using System;

    public class ProgramDetailsServiceModel : IMapFrom<Program>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DaysPerWeekTraining { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Trainer { get; set; }

        public int Trainees { get; set; }

        public int MaxTraineesCount { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Program, ProgramDetailsServiceModel>()
                .ForMember(c => c.Trainer, cfg => cfg.MapFrom(c => c.Trainer.UserName))
                .ForMember(c => c.Trainees, cfg => cfg.MapFrom(c => c.Clients.Count))
            .ForMember(c => c.MaxTraineesCount, cfg => cfg.MapFrom(c => c.MaxClientsNumber));
    }
}
