namespace FitnessDestiny.Web.Areas.Admin.Models.Store
{
    using FitnessDestiny.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class CreateSupplementFormModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public SupplementType SupplementType { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        
    }
}
