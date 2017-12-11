using FitnessDestiny.Core.Mapping;
using FitnessDestiny.Data.Models;
using FitnessDestiny.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessDestiny.Services.Admin.Models.Store
{
    public class SupplementEditServiceModel : IMapFrom<Supplement>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public string ImageUrl { get; set; }

        public SupplementType SupplementType { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool InStock { get; set; }
    }
}
