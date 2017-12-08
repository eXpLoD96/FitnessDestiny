using FitnessDestiny.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessDestiny.Web.Areas.Admin.Models.Store
{
    public class CreateSupplementFormModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public string ImageUrl { get; set; }

        public SupplementType SupplementType { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        
    }
}
