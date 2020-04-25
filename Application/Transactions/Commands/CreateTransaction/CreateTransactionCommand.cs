using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<(Result result, int? transactionId)>
    {
        public int AccountId { get; set; }
        public string Type { get; set; }
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
            var (result, transaction) = await _transactionService.MakeTransactionAsync(
                request.AccountId,
                request.Amount, 
                request.Operation, 
                request.Type, 
                request.Symbol, 
                request.Bank, 
                request.Account,
                cancellationToken)
                .ConfigureAwait(false);


            return result.Succeeded ? ((Result, int?)) (result, transaction.TransactionId) : (result, null);
        }
    }
}