using AutoMapper;
using BankMoneyLaunderer.Models;
using Domain.Entities;

namespace BankMoneyLaunderer.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            
            CreateMap<Account, AccountData>()
                .ForMember(s => s.Transactions,
                    map => map.MapFrom(s => s.Transactions));

            CreateMap<Account, AccountReport>()
                .ForMember(s => s.SuspiciousTransactions,
                    map =>
                        map.MapFrom(s => s.Transactions));
        }
    }
}