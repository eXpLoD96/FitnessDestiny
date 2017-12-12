namespace FitnessDestiny.Web.Models.Home
{
    using FitnessDestiny.Services.Blog.Models;
    using FitnessDestiny.Services.Models;
    using FitnessDestiny.Services.Store.Models;
    using System.Collections.Generic;

    public class SearchViewModel
    {
        public IEnumerable<ArticleListingServiceModel> Articles { get; set; }
            = new List<ArticleListingServiceModel>();

        public IEnumerable<SupplementListingServiceModel> Supplements { get; set; }
            = new List<SupplementListingServiceModel>();
        
        public IEnumerable<UserListingServiceModel> Users { get; set; }
            = new List<UserListingServiceModel>();

        public string SearchText { get; set; }
    }
}
