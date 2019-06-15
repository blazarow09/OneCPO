using AutoMapper;

namespace OneCPO.Services.Mapping.Contracts
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}