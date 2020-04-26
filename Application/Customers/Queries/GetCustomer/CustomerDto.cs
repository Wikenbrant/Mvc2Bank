using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Customers.Queries.GetCustomer
{
    public class CustomerDto : IMapFrom<Customer>
    {
        public int CustomerId { get; set; }
        public string Gender { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }

        public string Fullname => $"{Givenname} {Surname}";
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public DateTime? Birthday { get; set; }
        public string NationalId { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Telephonenumber { get; set; }
        public string Emailaddress { get; set; }


        public IEnumerable<Account> Accounts { get; set; } = new List<Account>();
        public decimal TotalBalance { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, CustomerDto>()

                .ForMember(s=>s.TotalBalance,
                    opt=> opt.MapFrom(s=>
                        s.Dispositions.Sum(d=>d.Account.Balance)))

                .ForMember(s => s.Accounts,
                    opt => opt.MapFrom(s => 
                        s.Dispositions.Select(d => d.Account).ToList()));
        }
    }
}
