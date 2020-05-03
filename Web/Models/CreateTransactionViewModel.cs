using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models
{
    public class CreateTransactionViewModel
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public string Type { get; set; }
        public List<SelectListItem> Types { get; set; }

        [Required]
        public string Operation { get; set; }

        public List<SelectListItem> Operations { get; set; }


        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Symbol { get; set; }
        public List<SelectListItem> Symbols { get; set; }

        [Required]
        public string Bank { get; set; }

        [Required]
        public string Account { get; set; }
    }
}