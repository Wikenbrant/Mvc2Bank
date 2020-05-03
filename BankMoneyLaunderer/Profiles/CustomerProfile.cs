using System.Linq;
using AutoMapper;
using BankMoneyLaunderer.Models;
using Domain.Entities;

namespace BankMoneyLaunderer.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerData>()
                .ForMember(s => s.Accounts,
                    map => 
                        map.MapFrom(s => s.Dispositions.Select(d => d.Account)));

            CreateMap<Customer, CustomerReport>()
                .ForMember(d => d.SuspiciousAccounts,
                    map =>
                        map.MapFrom(s => s.Dispositions.Select(d => d.Account)));
        }
    }
}