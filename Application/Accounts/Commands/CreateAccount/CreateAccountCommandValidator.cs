using Domain.Enums;
using FluentValidation;

namespace Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(account => account.Frequency)
                .IsEnumName(typeof(FrequencyType))
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}