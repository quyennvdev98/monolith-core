using Monolith.Core.Application.DTOs.Users;
using Monolith.Core.Domain.Entities;

namespace Monolith.Core.Application.Mappings;

public static class UserMappingExtensions
{
     public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            // Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            FullName = $"{user.FirstName} {user.LastName}",
            IsActive = user.IsActivated,
            // CreatedAt = user.CreatedAt,
            // UpdatedAt = user.UpdatedAt
        };
    }

    public static User ToEntity(this CreateUserDto dto)
    {
        return new User
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };
    }

    public static User ToEntity(this UpdateUserDto dto, User existingUser)
    {
        existingUser.Email = dto.Email;
        // existingUser.UpdateProfile(dto.FirstName, dto.LastName);
        // existingUser.IsActive = dto.IsActive;
        return existingUser;
    }
}