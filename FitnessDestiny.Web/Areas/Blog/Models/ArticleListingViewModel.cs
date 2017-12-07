namespace FitnessDestiny.Web.Areas.Blog.Models
{
    using FitnessDestiny.Services.Blog.Models;
    using System;
    using System.Collections.Generic;
    using static WebConstants;

    public class ArticleListingViewModel
    {
        public IEnumerable<ArticleListingServiceModel> Articles { get; set; }

        public int TotalArticles { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalArticles / PageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}
