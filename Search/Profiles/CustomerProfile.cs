using AutoMapper;
using Domain.Entities;
using Domain.SearchModels;

namespace Search.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerSearch>()
                .ForMember(s => s.CustomerId,
                    map => map.MapFrom(s => s.CustomerId.ToString()))
                //.ForMember(s => s.Dispositions,
                //    map => map.MapFrom(s => s.Dispositions))
                ;
        }
    }
}