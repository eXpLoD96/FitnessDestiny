namespace FitnessDestiny.Services.Blog.Models
{
    using AutoMapper;
    using FitnessDestiny.Core.Mapping;
    using FitnessDestiny.Data.Models;
    using System;

    public class ArticleListingServiceModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortContent { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Article, ArticleListingServiceModel>()
                .ForMember(a => a.Author, cfg => cfg.MapFrom(a => a.Author.UserName))
                .ForMember(sc => sc.ShortContent, cfg => cfg.MapFrom(c => c.Content.Substring(0, 200)));
    }
}
