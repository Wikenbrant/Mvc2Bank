using System.Linq;
using Domain.Enums;
using FluentValidation;

namespace Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(transaction => transaction.Type)
                .IsEnumName(typeof(TransactionType));

            RuleFor(transaction => transaction.Operation)
                .Must(operation =>
                    string.IsNullOrEmpty(operation) || OperationTypes.Operations.Contains(operation));

            RuleFor(transaction => transaction.Symbol)
                .Must(symbol =>
                    string.IsNullOrEmpty(symbol) || SymbolType.Symbols.Contains(symbol));

        }
    }
}