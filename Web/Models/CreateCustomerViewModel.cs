using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Mappings;
using Application.Customers.Queries.GetCustomer;
using Application.Customers.Queries.GetCustomers;
using Domain.Entities;

namespace Web.Models
{
    public class CreateCustomerViewModel : IMapFrom<Customer>, IMapFrom<CustomerDto>, IMapFrom<CustomersDto>
    {
        public List<string> Genders { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Givenname { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Streetaddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Zipcode { get; set; }

        public List<string> Countries { get; set; }
        [Required]
        public string Country { get; set; }

        public List<string> CountryCodes { get; set; }
        [Required]
        public string CountryCode { get; set; }

        public string Telephonecountrycode { get; set; }

        public string Telephonenumber { get; set; }

        [EmailAddress]
        public string Emailaddress { get; set; }
    }
}
