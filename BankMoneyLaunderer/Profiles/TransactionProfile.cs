using AutoMapper;
using BankMoneyLaunderer.Models;
using Domain.Entities;

namespace BankMoneyLaunderer.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionData>();
        }
    }
}