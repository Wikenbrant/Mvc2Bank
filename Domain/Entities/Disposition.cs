﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.Azure.Search;

namespace Domain.Entities
{
    public partial class Disposition
    {
        public Disposition()
        {
            Cards = new HashSet<Card>();
        }

        public int DispositionId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }

        public virtual Account Account { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
