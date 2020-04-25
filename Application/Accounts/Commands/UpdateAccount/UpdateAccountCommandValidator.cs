using Application.Common.Interfaces;
using Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountCommandValidator:AbstractValidator<UpdateAccountCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAccountCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(account => account.Id).NotEmpty().MustAsync( (id, cancellation) =>
                 _context.Accounts.AnyAsync(a => a.AccountId == id, cancellation));

            RuleFor(account => account.Frequency)
                .IsEnumName(typeof(FrequencyType))
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}