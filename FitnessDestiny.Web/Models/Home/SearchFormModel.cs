namespace FitnessDestiny.Web.Models.Home
{
    using System.ComponentModel.DataAnnotations;

    public class SearchFormModel
    {
        public string SearchText { get; set; }

        [Display(Name = "Search In Users")]
        public bool SearchInUsers { get; set; } = true;

        [Display(Name = "Search In Store")]
        public bool SearchInSupplements { get; set; } = true;

        [Display(Name = "Search In Articles")]
        public bool SearchInArticles { get; set; } = true;
    }

}
