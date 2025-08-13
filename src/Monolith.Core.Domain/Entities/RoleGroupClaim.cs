namespace Monolith.Core.Domain.Entities;

public sealed class RoleGroupClaim : Entity<Guid>
{
    public Guid RoleGroupId { get; set; }
    public RoleGroup RoleGroup { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}
