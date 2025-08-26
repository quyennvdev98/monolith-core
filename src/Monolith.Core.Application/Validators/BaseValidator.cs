using System.Linq.Expressions;
using FluentValidation;

namespace Monolith.Core.Application.Validators;

public abstract class BaseValidator<T> : AbstractValidator<T>
{
    protected BaseValidator()
    {
        ConfigureValidationRules();
    }

    protected abstract void ConfigureValidationRules();

    protected void ValidateId(Expression<Func<T, Guid>> expression, string entityName = "Entity")
    {
        RuleFor(expression)
            .NotEmpty()
            .WithMessage($"{entityName} ID is required")
            .WithErrorCode($"{entityName}.Id.Required")
            .NotEqual(Guid.Empty)
            .WithMessage($"{entityName} ID cannot be empty")
            .WithErrorCode($"{entityName}.Id.Empty");
    }

    protected void ValidateEmail(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithMessage("Email is required")
            .WithErrorCode("Email.Required")
            .EmailAddress()
            .WithMessage("Email format is invalid")
            .WithErrorCode("Email.InvalidFormat")
            .MaximumLength(254)
            .WithMessage("Email cannot exceed 254 characters")
            .WithErrorCode("Email.TooLong");
    }

    protected void ValidateName(Expression<Func<T, string>> expression, string fieldName, int maxLength = 50)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithMessage($"{fieldName} is required")
            .WithErrorCode($"{fieldName}.Required")
            .MaximumLength(maxLength)
            .WithMessage($"{fieldName} cannot exceed {maxLength} characters")
            .WithErrorCode($"{fieldName}.TooLong")
            .Matches(@"^[a-zA-Z\s\-'\.]+$")
            .WithMessage($"{fieldName} can only contain letters, spaces, hyphens, apostrophes, and periods")
            .WithErrorCode($"{fieldName}.InvalidCharacters");
    }
}