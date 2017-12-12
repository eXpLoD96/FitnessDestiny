namespace FitnessDestiny.Services.Store.Implementations
{
    using AutoMapper.QueryableExtensions;
    using FitnessDestiny.Data;
    using FitnessDestiny.Services.Store.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static FitnessDestiny.Services.ServiceConstants;

    public class StoreService : IStoreService
    {
        private readonly FitnessDestinyDbContext db;

        public StoreService(FitnessDestinyDbContext db)
        {
            this.db = db;
        }


        public async Task<IEnumerable<SupplementListingServiceModel>> AllAsync(int page = 1)
        => await this.db
                .Supplements
                .OrderByDescending(s => s.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ProjectTo<SupplementListingServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<SupplementListingServiceModel>> FindAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                .Supplements
                .OrderByDescending(c => c.Id)
                .Where(c => c.Name.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<SupplementListingServiceModel>()
                .ToListAsync();
        }

        public async Task<SupplementDetailsServiceModel> ByIdAsync(int id)
         => await this.db
                .Supplements
                .Where(s => s.Id == id)
                .ProjectTo<SupplementDetailsServiceModel>()
                .FirstOrDefaultAsync();

       

        public async Task<int> TotalAsync() => await this.db.Supplements.CountAsync();
    }
}
