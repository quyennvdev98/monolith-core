namespace Monolith.Core.Domain.Entities;
public sealed record RoleGroupClaimId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public override string ToString() => base.ToString();
}   
public sealed class RoleGroupClaim : EntityBase
{
    public RoleGroupClaimId Id { get; set; }    
    public string Key { get; set; }
    public string Value { get; set; }
    public RoleGroupId RoleGroupId { get; set; }
    
    public RoleGroup RoleGroup { get; set; }
}
