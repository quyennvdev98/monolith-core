namespace Monolith.Core.Domain.Entities;

public class RoleGroupMapRole : Entity<Guid>
{
    public Guid RoleGroupId { get; set; }
    public RoleGroup RoleGroup { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
}
