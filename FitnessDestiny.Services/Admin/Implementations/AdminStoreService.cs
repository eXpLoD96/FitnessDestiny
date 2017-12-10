using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FitnessDestiny.Data.Models.Enums;
using FitnessDestiny.Data;
using FitnessDestiny.Data.Models;
using System.Linq;

namespace FitnessDestiny.Services.Admin.Implementations
{
    public class AdminStoreService : IAdminStoreService
    {
        private readonly FitnessDestinyDbContext db;

        public AdminStoreService(FitnessDestinyDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string name, string description, string brand, string imageUrl, SupplementType supplementType, decimal price, int quantity)
        {
            var supplement = new Supplement
                {
                    Name = name,
                    Description = description,
                    Brand = brand,
                    ImageUrl = imageUrl,
                    SupplementType = supplementType,
                    Price = price,
                    Quantity = quantity
                };

            this.db.Supplements.Add(supplement);
            await this.db.SaveChangesAsync();
            
        }

        public bool Delete(int id)
        {
            var supplement = this.db
                .Supplements
                .Find(id);

            if(supplement == null)
            {
                return false;
            }

            this.db.Supplements.Remove(supplement);
            db.SaveChanges();

            return true;
        }
    }
}
