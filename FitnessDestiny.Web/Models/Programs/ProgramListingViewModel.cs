namespace FitnessDestiny.Web.Models.Programs
{
    using FitnessDestiny.Services.Models;
    using System;
    using System.Collections.Generic;

    using static FitnessDestiny.Services.ServiceConstants;

    public class ProgramListingViewModel
    {
        public IEnumerable<ProgramListingServiceModel> Programs { get; set; }

        public int TotalPrograms { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalPrograms / PageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}
