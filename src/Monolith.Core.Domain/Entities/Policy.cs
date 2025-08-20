namespace Monolith.Core.Domain.Entities;

public sealed record PolicyId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public override string ToString() => base.ToString();
}
public class Policy : EntityBase
{
    public PolicyId Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Resource { get; set; }
    public string Action { get; set; }
    public string? Condition { get; set; }
    public bool IsActive { get; set; }

    public List<PolicyMapRole> PolicyMapRoles { get; set; }

}