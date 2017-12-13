namespace FitnessDestiny.Services.Admin.Implementations
{
    using FitnessDestiny.Data;
    using FitnessDestiny.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class AdminProgramsService : IAdminProgramsService
    {
        private readonly FitnessDestinyDbContext db;

        public AdminProgramsService(FitnessDestinyDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(
            string name,
            string description,
            int daysPerWeek,
            DateTime startDate,
            DateTime endDate,
            string trainerId)
        {
            var program = new Program
            {
                Name = name,
                Description = description,
                DaysPerWeekTraining = daysPerWeek,
                StartDate = startDate,
                EndDate = endDate,
                TrainerId = trainerId
            };

            this.db.Add(program);

            await this.db.SaveChangesAsync();
        }
    }
}
