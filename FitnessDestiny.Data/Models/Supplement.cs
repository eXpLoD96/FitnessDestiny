namespace FitnessDestiny.Data.Models
{
    using FitnessDestiny.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Supplement
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(SupplementNameMinLength)]
        [MaxLength(SupplementNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(SupplementDescriptionMinLength)]
        [MaxLength(SupplementDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public SupplementType SupplementType { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool inStock { get; set; }

    }
}
