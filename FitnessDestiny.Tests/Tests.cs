namespace FitnessDestiny.Tests
{
    using AutoMapper;
    using FitnessDestiny.Web.Infrastructure.Mapper;

    public class Tests
    {
        private static bool testsInitialized = false;

        public static bool Initialize()
        {
            if (!testsInitialized)
            {
                Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
                testsInitialized = true;
                return true;
            }
            else
            {
                return true;
            }
        }

    }
}
