namespace FitnessDestiny.Services.Admin.Models.Store
{
    using FitnessDestiny.Core.Mapping;
    using FitnessDestiny.Data.Models;

    public class SupplementDeleteServiceModel : IMapFrom<Supplement>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }
    }
}
