namespace Monolith.Core.Domain.Entities;

public sealed class PolicyMapRole : EntityBase
{
    public PolicyId PolicyId { get; set; }
    public RoleId RoleId { get; set; }
    
    public Policy Policy { get; set; }
    public Role Role { get; set; }
}
