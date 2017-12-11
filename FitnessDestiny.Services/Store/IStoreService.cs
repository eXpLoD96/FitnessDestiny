namespace FitnessDestiny.Services.Store
{
    using FitnessDestiny.Services.Store.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStoreService
    {
        Task<IEnumerable<SupplementListingServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();

        Task<SupplementDetailsServiceModel> ByIdAsync(int id);
    }
}
