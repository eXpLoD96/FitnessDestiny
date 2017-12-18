namespace FitnessDestiny.Tests
{
    using AutoMapper;
    using FitnessDestiny.Web.Infrastructure.Mapper;

    public class Tests
    {
        private static bool testsInitialized = false;

        public static void Initialize()
        {
            if (!testsInitialized)
            {
                Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
                testsInitialized = true;
            }
        }

    }
}
