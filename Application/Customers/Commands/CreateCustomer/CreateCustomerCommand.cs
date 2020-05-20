using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<(Result result, int? customerId)>
    {
        public Customer Customer { get; set; }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, (Result result, int? customerId)>
    {
        private readonly IApplicationDbContext _context;

        public CreateCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<(Result result, int? customerId)> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Customer;

            _context.Customers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return (Result.Success(), entity.CustomerId);
        }
    }
}
