namespace FitnessDestiny.Web.Models.Programs
{
    using FitnessDestiny.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProgramDetailsViewModel
    {
        public ProgramDetailsServiceModel Program { get; set; }

        public bool UserIsEnrolledProgram { get; set; }
    }
}
