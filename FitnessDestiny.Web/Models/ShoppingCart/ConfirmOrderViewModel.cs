namespace FitnessDestiny.Web.Models.ShoppingCart
{
    using AutoMapper;
    using FitnessDestiny.Core.Mapping;
    using FitnessDestiny.Data.Models;
    using System.Collections.Generic;

    public class ConfirmOrderViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }

        public string FirstName {get;set;}

        public string LastName { get; set; }

        public string Email { get; set; }
        
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        
    }
}
