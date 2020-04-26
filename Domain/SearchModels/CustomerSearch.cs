using System;
using System.Collections.Generic;
using Microsoft.Azure.Search;

namespace Domain.SearchModels
{
    public partial class CustomerSearch
    {
        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable]
        public string CustomerId { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Gender { get; set; }

        [IsSearchable, IsSortable]
        public string Givenname { get; set; }

        [IsSearchable, IsSortable]
        public string Surname { get; set; }

        [IsSearchable, IsSortable]
        public string Streetaddress { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string City { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Zipcode { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Country { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string CountryCode { get; set; }

        [IsFilterable]
        public string NationalId { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Telephonecountrycode { get; set; }

        [IsSearchable, IsSortable, IsFacetable]
        public string Telephonenumber { get; set; }

        [IsSearchable, IsSortable, IsFacetable]
        public string Emailaddress { get; set; }

        public virtual ICollection<DispositionSearch> Dispositions { get; set; }
    }
}