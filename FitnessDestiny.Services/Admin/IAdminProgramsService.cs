using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDestiny.Services.Admin
{
    public interface IAdminProgramsService
    {
        Task CreateAsync(
            string name,
            string description,
            int daysPerWeek,
            DateTime startDate,
            DateTime endDate,
            string trainerId);
    }
}
