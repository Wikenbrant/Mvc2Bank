using System;
using System.Collections.Generic;

namespace Domain.Enums
{
    public static class OperationTypes
    {
        public static IEnumerable<string> Operations = new[]
        {
            None, 
            CollectionFromAnotherBank, 
            CreditCardWithdrawal, 
            CreditInCash, 
            RemittanceToAnotherBank,
            WithdrawalInCash
        };
        public static string None => String.Empty;
        public static string CollectionFromAnotherBank => "Collection from Another Bank";
        public static string CreditCardWithdrawal => "Credit Card Withdrawal";
        public static string CreditInCash => "Credit in Cash";
        public static string RemittanceToAnotherBank => "Remittance to Another Bank";
        public static string WithdrawalInCash => "Withdrawal in Cash";
    }
}