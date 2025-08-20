namespace Monolith.Core.Domain.Entities;

public class RoleGroupMapRole : EntityBase
{
    public RoleGroupId RoleGroupId { get; set; }
    public RoleId RoleId { get; set; }

    public RoleGroup RoleGroup { get; set; }
    public Role Role { get; set; }
}
