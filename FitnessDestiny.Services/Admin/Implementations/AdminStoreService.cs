namespace FitnessDestiny.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using FitnessDestiny.Data;
    using FitnessDestiny.Data.Models;
    using FitnessDestiny.Data.Models.Enums;
    using FitnessDestiny.Services.Admin.Models.Store;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<SupplementEditServiceModel> EditByIdAsync(int id)
          => await this.db
            .Supplements
            .Where(s => s.Id == id)
            .ProjectTo<SupplementEditServiceModel>()
            .FirstOrDefaultAsync();

        public async Task<bool> EditAsync(int id, string name, string description, string brand,
            string imageUrl, SupplementType supplementType, decimal price, int quantity, bool inStock)
        {
            var supplement = await this.db.Supplements.Where(a => a.Id == id).FirstOrDefaultAsync();

            if (supplement == null)
            {
                return false;
            }

            supplement.Name = name;
            supplement.Description = description;
            supplement.Brand = brand;
            supplement.ImageUrl = imageUrl;
            supplement.SupplementType = supplementType;
            supplement.Price = price;
            supplement.Quantity = quantity;
            supplement.inStock = inStock;

            await this.db.SaveChangesAsync();
            return true;
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
