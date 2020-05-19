using Domain.Enums;
using Domain.SearchModels;

namespace Web.Models
{
    public class CustomerTableViewModel
    {
        public int Page { get; set; }

        public OrderByType OrderByType { get; set; }

        public string OrderByField { get; set; }

        public SearchData SearchData { get; set; }
    }
}