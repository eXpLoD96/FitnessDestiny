namespace FitnessDestiny.Tests.Mocks
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using Moq;
    using FitnessDestiny.Data.Models;

    public class UserManagerMock
    {
        public static Mock<UserManager<User>> New
            => new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

    }
}
