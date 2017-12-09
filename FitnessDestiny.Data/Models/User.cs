namespace FitnessDestiny.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public List<Article> Articles { get; set; } = new List<Article>();

        public List<Program> ProgramsTrained{ get; set; } = new List<Program>();

        public List<TraineeProgram> ProgramsEnrolled { get; set; } = new List<TraineeProgram>();
    }
}
