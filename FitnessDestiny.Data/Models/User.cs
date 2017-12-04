namespace FitnessDestiny.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;

    public class User : IdentityUser
    {
        public string Username { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
