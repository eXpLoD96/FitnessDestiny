﻿namespace FitnessDestiny.Services.Blog.Models
{
    using AutoMapper;
    using FitnessDestiny.Core.Mapping;
    using FitnessDestiny.Data.Models;
    using System;

    public class ArticleDeleteServiceModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Article, ArticleDetailsServiceModel>()
                .ForMember(a => a.Author, cfg => cfg.MapFrom(a => a.Author.UserName));
    }
}
