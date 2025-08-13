namespace Monolith.Core.Domain.Entities;

public class Policy : Entity<Guid>
{
    public string Name { get; set; }
    public string Value { get; set; }
    public int Priority { get; set; } = 100;
    public string? Description { get; set; }
    public List<PolicyMapRole> PolicyMapRoles { get; set; }
    
}