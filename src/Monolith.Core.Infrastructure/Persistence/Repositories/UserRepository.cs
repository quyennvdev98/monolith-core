using FluentResults;
using Monolith.Core.Application.Abstractions.Repositories;
using Monolith.Core.Domain.Entities;
using Monolith.Core.Infrastructure.Repositories;
using Monolith.Core.Persistence;
using Serilog;

namespace Monolith.Core.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(CoreDbContext dbContext, ILogger logger) : EfRepository<User>(dbContext, logger), IUserRepository
{
    public Task<Result> AddToRolesAsync(User user, IEnumerable<string> rolesName)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> ChangePasswordAsync(User user, string currentPassword, string newPassword)
    {
        var userPassword = await GetFirstByConditionAsync(u => u.Id == user.Id && u.PasswordHash == currentPassword && user.IsActivated);
        if (userPassword is null)
        {
            return Result.Fail("User not found or invalid password");
        }

        userPassword.PasswordHash = newPassword;
        // await UpdateAsync(userPassword);

        return Result.Ok(true);
    }

    public Task<Result<bool>> ConfirmEmailAsync(User user, string token)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> ConfirmEmailResetPassAsync(User user, string commandToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> ConfirmPhoneNumberAsync(User user, string token)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<string>> GenerateChangePhoneNumberTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<Result<string>> GenerateEmailConfirmationTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<Result<string>> GenerateEmailResetPassTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<Result<string>> GeneratePasswordResetTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<Result<User>> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IList<string>>> GetRolesAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> RemoveFromRolesAsync(User user, IEnumerable<string> rolesName)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RemoveUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> ResetPasswordAsync(User user, string tokenPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> SetLockoutEnabledAsync(User user, bool enabled)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> SetLockoutEndDateAsync(User user, DateTimeOffset? lockUntil)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> ValidatePasswordAsync(User user, string password, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}