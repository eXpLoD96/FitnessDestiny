namespace FitnessDestiny.Tests.Services
{
    using Xunit;
    using FluentAssertions;
    using System.Threading.Tasks;
    using FitnessDestiny.Services.Implementations;
    using FitnessDestiny.Data.Models;
    using System.Linq;
    using FitnessDestiny.Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class ProgramsServiceTests
    {
        public ProgramsServiceTests()
        {
            Tests.Initialize();
        }
        

        [Fact]
        public async Task FindAsyncShouldReturnCorrectResultWithFilterAndOrder()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstCourse = new Program { Id = 1, Name = "First" };
            var secondCourse = new Program { Id = 2, Name = "Second" };
            var thirdCourse = new Program { Id = 3, Name = "Third" };

            db.AddRange(firstCourse, secondCourse, thirdCourse);

            await db.SaveChangesAsync();

            var programService = new ProgramService(db);

            // Act
            var result = await programService.FindAsync("t");

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 3
                    && r.ElementAt(1).Id == 1)
                .And
                .HaveCount(2);
        }
        

        [Fact]
        public async Task SignUpTraineeAsyncShouldSaveCorrectDataWithValidProgramIdAndTraineeId()
        {
            // Arrange
            var db = this.GetDatabase();

            const int programId = 1;
            const string traineeId = "TestTrainee";

            var program = new Program
            {
                Id = programId,
                StartDate = DateTime.MaxValue,
                Clients = new List<TraineeProgram>()
            };

            db.Add(program);

            await db.SaveChangesAsync();

            var programService = new ProgramService(db);

            // Act
            var result = await programService.SignUpUserAsync(programId, traineeId);
            var savedEntry = db.Find<TraineeProgram>(programId, traineeId);

            // Assert
            result
                .Should()
                .Be(true);

            savedEntry
                .Should()
                .NotBeNull();
        }

        [Fact]
        public async Task SignOutTraineeAsyncShouldSaveCorrectDataWithValidProgramIdAndTraineeId()
        {
            // Arrange
            var db = this.GetDatabase();
            var programService = new ProgramService(db);

            const int programId = 1;
            const string traineeId = "TestTrainee";

            var program = new Program
            {
                Id = programId,
                StartDate = DateTime.MaxValue,
                Clients = new List<TraineeProgram>()
            };
            db.Add(program);
            await db.SaveChangesAsync();

            // Act
            var result = await programService.SignUpUserAsync(programId, traineeId);
            var signOut = await programService.SignOutUserAsync(programId, traineeId);

            // Assert
            result
                .Should()
                .Be(true);

            signOut
                .Should()
                .Be(true);
        }

            private FitnessDestinyDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<FitnessDestinyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new FitnessDestinyDbContext(dbOptions);
        }


    }
}
