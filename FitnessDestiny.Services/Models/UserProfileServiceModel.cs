namespace FitnessDestiny.Services.Models
{
    using AutoMapper;
    using FitnessDestiny.Core.Mapping;
    using FitnessDestiny.Data.Models;
    using FitnessDestiny.Services.Blog.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class UserProfileServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public IEnumerable<UserProfileProgramServiceModel> Programs { get; set; }

        public IEnumerable<ArticleDetailsServiceModel> Articles { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            string userName = this.UserName;
            mapper
                .CreateMap<User, UserProfileServiceModel>()
                .ForMember(up => up.Articles, cfg => cfg.MapFrom(u => u.Articles.Where(s => s.Author.UserName ==  userName).Select(s => s.Title)))
                .ForMember(up => up.UserName, cfg => cfg.MapFrom(u => u.UserName))
                .ForMember(u => u.Programs, cfg => cfg.MapFrom(s => s.ProgramsEnrolled.Select(c => c.Program.Name)))
            .ForMember(u => u.Articles, cfg => cfg.MapFrom(a => a.Articles.Select(ar => ar.Title)));
        }
    }
}
