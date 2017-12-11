namespace FitnessDestiny.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using FitnessDestiny.Data;
    using FitnessDestiny.Services.Admin.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdminUserService : IAdminUserService
    {
        private FitnessDestinyDbContext db;

        public AdminUserService(FitnessDestinyDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
            => await this.db
                .Users
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();
    }
}
