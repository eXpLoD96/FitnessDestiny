namespace FitnessDestiny.Web.Areas.Store.Models
{
    using FitnessDestiny.Services.Store.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static FitnessDestiny.Services.ServiceConstants;

    public class SupplementListingViewModel
    {
        public IEnumerable<SupplementListingServiceModel> Supplements { get; set; }

        public int TotalSupplements { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalSupplements / PageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}
