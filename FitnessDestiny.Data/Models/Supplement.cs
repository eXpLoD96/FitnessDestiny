namespace FitnessDestiny.Data.Models
{
    using FitnessDestiny.Data.Models.Enums;

    public class Supplement
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public SupplementType SupplementType { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool inStock { get; set; }

    }
}
