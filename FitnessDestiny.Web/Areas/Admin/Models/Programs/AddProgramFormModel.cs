namespace FitnessDestiny.Web.Areas.Admin.Models.Programs
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using static Data.DataConstants;

    public class AddProgramFormModel : IValidatableObject
    {
        [Required]
        [MaxLength(ProgramNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ProgramDescriptionLength)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int DaysPerWeek { get; set; }

        [Display(Name = "Trainer")]
        [Required]
        public string TrainerId { get; set; }

        public IEnumerable<SelectListItem> Trainers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate < DateTime.UtcNow)
            {
                yield return new ValidationResult("Start date should be in the future.");
            }

            if (this.StartDate > this.EndDate)
            {
                yield return new ValidationResult("Start date should be before end date.");
            }
        }
    }
}

