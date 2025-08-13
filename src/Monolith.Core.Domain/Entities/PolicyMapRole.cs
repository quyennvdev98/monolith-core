namespace Monolith.Core.Domain.Entities;

public sealed class PolicyMapRole : Entity<Guid>
{
    public Guid PolicyId { get; set; }
    public Policy Policy { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
}
