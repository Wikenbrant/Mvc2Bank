using AutoMapper;
using Domain.Entities;
using Domain.SearchModels;

namespace Search.Profiles
{
    public class DepositionProfile: Profile
    {
        public DepositionProfile()
        {
            CreateMap<Disposition, DispositionSearch>()
                .ForMember(s => s.Account,
                    map => map.MapFrom(s => s.Account));
        }
    }
}