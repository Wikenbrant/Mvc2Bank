using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<int>
    {
        public string Frequency { get; set; }
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = new Account
            {
                Frequency = request.Frequency,
                Balance = 0,
                Created = DateTime.Now
            };
            _context.Accounts.Add(entity);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return entity.AccountId;
        }
    }
}