using Monolith.Core.Domain.Common.Errors;

namespace Monolith.Core.Domain.DomainErrors;

public static class UserErrors
{
    public static DomainError NotFound(Guid id) =>
            DomainError.NotFound(
                "User.NotFound", 
                $"User with ID '{id}' was not found")
                .WithMetadata("UserId", id);

        public static DomainError EmailAlreadyExists(string email) =>
            DomainError.Conflict(
                "User.EmailExists", 
                $"A user with email '{email}' already exists", 
                "Email")
                .WithMetadata("Email", email);

        public static DomainError InvalidEmail(string email) =>
            DomainError.Validation(
                "User.InvalidEmail", 
                $"Email '{email}' has an invalid format", 
                "Email");

        public static DomainError InactiveUser(Guid id) =>
            DomainError.Conflict(
                "User.Inactive", 
                $"User with ID '{id}' is inactive")
                .WithMetadata("UserId", id);
}