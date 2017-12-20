using FitnessDestiny.Core.Mapping;
using FitnessDestiny.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessDestiny.Services.Models
{
    public class CartItem
    {
        public int SupplementId { get; set; }

        public int Quantity { get; set; }
    }
}
