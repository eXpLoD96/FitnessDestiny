using System;
using System.Collections.Generic;
using System.Text;
using FitnessDestiny.Services.Admin.Models;
using FitnessDestiny.Data;
using AutoMapper.QueryableExtensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FitnessDestiny.Services.Admin.Implementations
{
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
