using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using MediatR;

namespace Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<(Result result, int? transactionId)>
    {
        public int AccountId { get; set; }
        public TransactionType Type { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
        public string Symbol { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, (Result result, int? transactionId)>
    {
        private readonly ITransactionService _transactionService;

        public CreateTransactionCommandHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        public async Task<(Result, int?)> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var (result, transaction) = request.Type switch
            {
                TransactionType.Credit => await _transactionService.MakeDepositAsync(request.AccountId,
                    request.Amount,
                    request.Operation,
                    request.Symbol,
                    request.Bank,
                    request.Account,
                    cancellationToken).ConfigureAwait(false),
                TransactionType.Debit => await _transactionService.MakeWithdrawalAsync(request.AccountId,
                    request.Amount,
                    request.Operation,
                    request.Symbol,
                    request.Bank,
                    request.Account,
                    cancellationToken).ConfigureAwait(false),
                _ => throw new ArgumentNullException(nameof(request.Type))
            };


            return (result, transaction?.TransactionId);
        }
    }
}