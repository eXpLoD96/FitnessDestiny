namespace FitnessDestiny.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using FitnessDestiny.Data;
    using FitnessDestiny.Data.Models;
    using FitnessDestiny.Services.Models;
    using FitnessDestiny.Services.Store.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using static FitnessDestiny.Services.ServiceConstants;

    public class ProgramService : IProgramService
    {
        private readonly FitnessDestinyDbContext db;

        public ProgramService(FitnessDestinyDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ProgramListingServiceModel>> AllAsync(int page = 1)
        => await this.db
                .Programs
                .OrderByDescending(s => s.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ProjectTo<ProgramListingServiceModel>()
                .ToListAsync();

        public async Task<int> TotalAsync() => await this.db.Programs.CountAsync();

        public async Task<IEnumerable<ProgramListingServiceModel>> ActiveAsync()
            => await this.db
                .Programs
                .OrderByDescending(c => c.Id)
                .Where(c => c.StartDate >= DateTime.UtcNow)
                .ProjectTo<ProgramListingServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<ProgramListingServiceModel>> FindAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                .Programs
                .OrderByDescending(c => c.Id)
                .Where(c => c.Name.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<ProgramListingServiceModel>()
                .ToListAsync();
        }

        public async Task<TModel> ByIdAsync<TModel>(int id) where TModel : class
            => await this.db
                .Programs
                .Where(c => c.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> SignUpUserAsync(int programId, string userId)
        {
            var programInfo = await this.GetProgramInfo(programId, userId);

            if (programInfo == null
                || programInfo.StartDate < DateTime.UtcNow
                || programInfo.UserIsEnrolled)
            {
                return false;
            }

            var trainee = new TraineeProgram
            {
                ProgramId = programId,
                TraineeId = userId
            };

            this.db.Add(trainee);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SignOutUserAsync(int programId, string traineeId)
        {
            var programInfo = await this.GetProgramInfo(programId, traineeId);

            if (programInfo == null
                || programInfo.StartDate < DateTime.UtcNow
                || !programInfo.UserIsEnrolled)
            {
                return false;
            }

            var traineeInProgram = await this.db
                .FindAsync<TraineeProgram>(programId, traineeId);

            this.db.Remove(traineeInProgram);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UserIsEnrolledProgramAsync(int programId, string traineeId)
            => await this.db
                .Programs
                .AnyAsync(c => c.Id == programId && c.Clients.Any(s => s.TraineeId == traineeId));

        private async Task<ProgramWithTrainees> GetProgramInfo(int programId, string studentId)
            => await this.db
                .Programs
                .Where(c => c.Id == programId)
                .Select(c => new ProgramWithTrainees
                {
                    StartDate = c.StartDate,
                    UserIsEnrolled = c.Clients.Any(s => s.TraineeId == studentId)
                })
                .FirstOrDefaultAsync();

        
    }
}
