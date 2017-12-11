namespace FitnessDestiny.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using static DataConstants;

    public class Program
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ProgramNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ProgramDescriptionLength)]
        public string Description { get; set; }
        
        public int DaysPerWeekTraining { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TrainerId { get; set; }

        public User Trainer { get; set; }

        public List<TraineeProgram> Clients { get; set; } = new List<TraineeProgram>();

        public int MaxClientsNumber { get; set; }

        public int CurrentClientsNumber { get; set; }
    }
}
