using FitnessDestiny.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDestiny.Services.Admin
{
    public interface IAdminStoreService
    {
        Task CreateAsync(string name, string description, string brand, string imageUrl, SupplementType supplementType, decimal price, int Quantity);

        bool Delete(int id);
    }
}
