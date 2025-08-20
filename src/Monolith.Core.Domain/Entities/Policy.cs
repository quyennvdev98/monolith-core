namespace Monolith.Core.Domain.Entities;

public class Policy : Entity<Guid>
{
   
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public string Resource { get; set; } = string.Empty;
    
    public string Action { get; set; } = string.Empty;
    
    public string? Condition { get; set; }

    public bool IsActive { get; set; }
    public List<PolicyMapRole> PolicyMapRoles { get; set; }
    
}