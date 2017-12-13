namespace FitnessDestiny.Services
{
    using FitnessDestiny.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IProgramService
    {
        Task<IEnumerable<ProgramListingServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();

        Task<IEnumerable<ProgramListingServiceModel>> ActiveAsync();

        Task<IEnumerable<ProgramListingServiceModel>> FindAsync(string searchText);

        Task<TModel> ByIdAsync<TModel>(int id) where TModel : class;

        Task<bool> SignUpUserAsync(int courseId, string studentId);

        Task<bool> SignOutUserAsync(int courseId, string studentId);

        Task<bool> UserIsEnrolledProgramAsync(int courseId, string studentId);
    }
}
