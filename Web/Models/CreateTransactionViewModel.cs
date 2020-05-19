using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CreateTransactionViewModel
    {
        [Required]
        public int? AccountId { get; set; }

        public IEnumerable<int> AccountIds { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Operation { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Symbol { get; set; }

        [Display(Name = "To bank")]
        public string Bank { get; set; }

        [Display(Name = "To account")]
        public int? Account { get; set; }
    }
}