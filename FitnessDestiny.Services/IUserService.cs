namespace FitnessDestiny.Services
{
    using FitnessDestiny.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<IEnumerable<UserListingServiceModel>> FindAsync(string searchText);
    }
}
