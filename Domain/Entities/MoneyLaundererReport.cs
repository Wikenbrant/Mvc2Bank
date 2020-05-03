using System;

namespace Domain.Entities
{
    public class MoneyLaundererReport
    {
        public int MoneyLaundererId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Succeeded { get; set; } = false;
    }
}