namespace FitnessDestiny.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using FitnessDestiny.Data;
    using FitnessDestiny.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    class UserService : IUserService
    {
        private readonly FitnessDestinyDbContext db;

        public UserService(FitnessDestinyDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<UserListingServiceModel>> FindAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                .Users
                .OrderBy(u => u.UserName)
                .Where(u => u.UserName.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<UserListingServiceModel>()
                .ToListAsync();
        }
    }
}
