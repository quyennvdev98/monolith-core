
namespace Monolith.Core.Domain.Entities;

public sealed record UserId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public override string ToString() => base.ToString();
}
public sealed class User : EntityBase
{
    public UserId Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string? NormalizedUserName { get; set; }
    public string? Email { get; set; }
    public string? NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? PasswordHash { get; set; }
    public string? SecurityStamp { get; set; }
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    public string SearchHint { get; set; }
    public string Avatar { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? ChangedPasswordTime { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public bool IsActivated { get; set; }
    
    public List<UserMapRoleGroup> UserRoleGroups { get; set; }

}