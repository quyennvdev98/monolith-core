using FluentResults;
using Monolith.Core.Domain.Entities;

namespace Monolith.Core.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<Result<User>> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Result<bool>> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<Result<bool>> SetLockoutEnabledAsync(User user, bool enabled);
    Task<Result<string>> GenerateEmailConfirmationTokenAsync(User user);
    Task<Result<bool>> ValidatePasswordAsync(User user, string password,
        CancellationToken cancellationToken);
    Task<Result<string>> GenerateChangePhoneNumberTokenAsync(User user);
    Task<Result<bool>> ConfirmEmailAsync(User user, string token);
    Task<Result<bool>> ConfirmPhoneNumberAsync(User user, string token);
    Task<Result<bool>> SetLockoutEndDateAsync(User user,
        DateTimeOffset? lockUntil);
    Task<Result<bool>> ChangePasswordAsync(User user, string currentPassword,
        string newPassword);
    Task<Result<string>> GeneratePasswordResetTokenAsync(User user);
    Task<Result<IList<string>>> GetRolesAsync(User user);
    Task<Result> AddToRolesAsync(User user,
        IEnumerable<string> rolesName);
    Task<Result<bool>> RemoveFromRolesAsync(User user, IEnumerable<string> rolesName);
    Task<Result<bool>> ResetPasswordAsync(User user, string tokenPassword,
        string newPassword);
    Task<Result> RemoveUser(User user);
    Task<Result<string>> GenerateEmailResetPassTokenAsync(User user);
    Task<Result<bool>> ConfirmEmailResetPassAsync(User user, string commandToken);
}