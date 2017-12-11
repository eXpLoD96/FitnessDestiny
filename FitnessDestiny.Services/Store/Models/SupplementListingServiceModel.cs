namespace FitnessDestiny.Services.Store.Models
{
    using AutoMapper;
    using FitnessDestiny.Core.Mapping;
    using FitnessDestiny.Data.Models;
    using FitnessDestiny.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SupplementListingServiceModel : IMapFrom<Supplement>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public SupplementType SupplementType { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public bool InStock { get; set; }

        public string Brand { get; set; }
        
    }
}

