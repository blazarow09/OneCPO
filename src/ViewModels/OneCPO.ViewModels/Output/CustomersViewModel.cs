using AutoMapper;
using OneCPO.Data.Models;
using OneCPO.Data.Models.Enums;
using OneCPO.Services.Mapping.Contracts;
using System;

namespace OneCPO.ViewModels.Output
{
    public class CustomersViewModel : IMapFrom<Customer>, IHaveCustomMappings
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public GenderType Gender { get; set; }

        public string Telephone { get; set; }

        public DateTime CreatedOn { get; set; }

        public StatusType Status { get; set; }

        //TODO check for purchase orders

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //configuration.CreateMap<Customer, CustomersViewModel>()
            //    .ForMember(x => x.CountOfAllJokes,
            //        m => m.MapFrom(c => c.Jokes.Count()));
        }
    }
}