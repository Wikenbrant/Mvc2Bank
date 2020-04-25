using System;
using System.Buffers;
using System.Collections.Generic;

namespace Domain.Enums
{
    public static class SymbolType
    {
        public static IEnumerable<string> Symbols = new[]
        {
            None,
            Null,
            Household,
            InsurrancePayment,
            InterestCredited,
            LoanPayment,
            OldAgePension,
            PaymentForStatement,
            SanctionInterestIfNegativeBalance
        };
        
        public static string None => String.Empty;
        public static string Null => null;
        public static string Household = "household";
        public static string InsurrancePayment= "insurrance payment";
        public static string InterestCredited = "interest credited";
        public static string LoanPayment = "loan payment";
        public static string OldAgePension = "old-age pension";
        public static string PaymentForStatement = "payment for statement";
        public static string SanctionInterestIfNegativeBalance = "sanction interest if negative balance";

    }
}