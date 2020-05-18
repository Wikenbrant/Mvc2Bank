using System;
using AutoMapper;
using Domain.Entities;
using Domain.SearchModels;

namespace Search.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountSearch>()
                .ForMember(s => s.Balance,
                    map => map.MapFrom(s => Convert.ToDouble(s.Balance)));
        }
    }
}