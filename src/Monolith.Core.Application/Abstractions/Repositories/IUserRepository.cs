using Monolith.Core.Domain.Entities;

namespace Monolith.Core.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<bool> SetLockoutEnabledAsync(User user, bool enabled);
    Task<string> GenerateEmailConfirmationTokenAsync(User user);
    Task<bool> ValidatePasswordAsync(User user, string password,
        CancellationToken cancellationToken);
    Task<string> GenerateChangePhoneNumberTokenAsync(User user);
    Task<bool> ConfirmEmailAsync(User user, string token);
    Task<bool> ConfirmPhoneNumberAsync(User user, string token);

    Task<bool> SetLockoutEndDateAsync(User user,
        DateTimeOffset? lockUntil);
    Task<bool> ChangePasswordAsync(User user, string currentPassword,
        string newPassword);
    Task<string> GeneratePasswordResetTokenAsync(User user);
    Task<IList<string>> GetRolesAsync(User user);
    Task AddToRolesAsync(User user,
        IEnumerable<string> rolesName);
    Task<bool> RemoveFromRolesAsync(User user, IEnumerable<string> rolesName);
    Task<bool> ResetPasswordAsync(User user, string tokenPassword,
        string newPassword);
    Task RemoveUser(User user);
    Task<string> GenerateEmailResetPassTokenAsync(User user);
    Task<bool> ConfirmEmailResetPassAsync(User user, string commandToken);
}