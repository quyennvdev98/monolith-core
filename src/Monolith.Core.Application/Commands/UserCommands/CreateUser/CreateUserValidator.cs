using FluentValidation;
using Monolith.Core.Application.Abstractions.Repositories;
using Monolith.Core.Application.DTOs.Users;
using Monolith.Core.Application.Validators;

namespace Monolith.Core.Application.Commands.UserCommands.CreateUser;

public class CreateUserValidator : BaseValidator<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override void ConfigureValidationRules()
    {
        ValidateEmail(x => x.Email);
        ValidateName(x => x.FirstName, "First Name");
        ValidateName(x => x.LastName, "Last Name");

        RuleFor(x => x.Email)
            .MustAsync(BeUniqueEmail)
            .WithMessage("A user with this email already exists")
            .WithErrorCode("Email.AlreadyExists")
            .When(x => !string.IsNullOrEmpty(x.Email));
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return  await Task.FromResult(true);
    }
}