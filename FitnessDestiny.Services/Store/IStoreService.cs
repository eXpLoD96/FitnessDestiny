using FitnessDestiny.Services.Store.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDestiny.Services.Store
{
    public interface IStoreService
    {
        Task<IEnumerable<SupplementListingServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();

        Task<SupplementDetailsServiceModel> ByIdAsync(int id);
    }
}
