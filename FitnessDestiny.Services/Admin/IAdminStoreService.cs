using FitnessDestiny.Data.Models.Enums;
using FitnessDestiny.Services.Admin.Models.Store;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDestiny.Services.Admin
{
    public interface IAdminStoreService
    {
        Task CreateAsync(string name, string description, string brand, string imageUrl, SupplementType supplementType, decimal price, int quantity);

        bool Delete(int id);

        Task<SupplementEditServiceModel> EditByIdAsync(int id);

        Task<bool> EditAsync(int id, string name, string description, string brand, string imageUrl, SupplementType supplementType, decimal price, int quantity, bool inStock);
    }
}
