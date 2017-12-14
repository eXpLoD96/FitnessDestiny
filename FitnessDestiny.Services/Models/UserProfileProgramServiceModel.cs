namespace FitnessDestiny.Services.Models
{
    using AutoMapper;
    using FitnessDestiny.Core.Mapping;
    using FitnessDestiny.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public class UserProfileProgramServiceModel : IMapFrom<Program>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DaysPerWeekTraining { get; set; }

        public User Trainer { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            string userId = null;
            mapper
                .CreateMap<Program, UserProfileProgramServiceModel>()
                .ForMember(up => up.Name, cfg => cfg.MapFrom(u => u.Clients.Where(s => s.TraineeId == userId).Select(s => s.Program.Name)))
                .ForMember(up => up.Trainer, cfg => cfg
                    .MapFrom(p => p.Trainer.UserName));
        }
    }

}
