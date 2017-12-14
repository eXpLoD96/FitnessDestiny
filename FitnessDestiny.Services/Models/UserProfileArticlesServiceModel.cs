using AutoMapper;
using FitnessDestiny.Core.Mapping;
using FitnessDestiny.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessDestiny.Services.Models
{
    public class UserProfileArticlesServiceModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Article, UserProfileArticlesServiceModel>()
                .ForMember(a => a.Author, cfg => cfg.MapFrom(a => a.Author.UserName));
        }
    }
}
