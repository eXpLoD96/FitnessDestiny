namespace FitnessDestiny.Web.Infrastructure.Mapper
{
    using AutoMapper;

    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
