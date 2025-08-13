namespace Monolith.Core.Domain.Entities;

public sealed class UserMapRoleGroup : Entity<Guid>
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid RoleGroupId { get; set; }
    public RoleGroup RoleGroup { get; set; }
    
   
}   