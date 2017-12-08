namespace FitnessDestiny.Services.Blog.Models
{
    using FitnessDestiny.Core.Mapping;
    using FitnessDestiny.Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using static FitnessDestiny.Data.DataConstants;
    using AutoMapper;

    public class ArticleEditServiceModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public int Id { get; set; }
        [Required]
        [MinLength(ArticleTitleMinLength)]
        [MaxLength(ArticleTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ArticleContentMinLength)]
        [MaxLength(ArticleContentMaxLength)]
        public string Content { get; set; }
       
        public User User { get; set; }

        public void ConfigureMapping(Profile mapper)
        => mapper
                .CreateMap<Article, ArticleEditServiceModel>()
                .ForMember(a => a.User, cfg => cfg.MapFrom(a => a.Author.UserName));
    }
}
