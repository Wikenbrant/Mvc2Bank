using System.Linq;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Customers.Queries.GetTop10CustomersByCountry
{
    public class Top10CustomersByCountryDto : IMapFrom<Customer>
    {
        public int CustomerId { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public decimal Total { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, Top10CustomersByCountryDto>()
                .ForMember(d => d.Total,
                    opt => 
                        opt.MapFrom(s => s.Dispositions.Sum(disposition => disposition.Account.Balance)));
        }
    }
}