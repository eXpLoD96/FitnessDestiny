namespace FitnessDestiny.Web.Areas.Blog.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class PublishArticleFormModel
    {
        [Required]
        [MinLength(ArticleTitleMinLength)]
        [MaxLength(ArticleTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ArticleContentMinLength)]
        [MaxLength(ArticleContentMaxLength)]
        public string Content { get; set; }
    }
}
