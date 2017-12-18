using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessDestiny.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set;}

        public User User { get; set; }

        //Address

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public decimal TotalPrice { get; set; }
    }
}
