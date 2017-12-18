using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessDestiny.Data.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int SupplementId { get; set; }

        public decimal SupplementPrice { get; set; }

        public int Quantity { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
