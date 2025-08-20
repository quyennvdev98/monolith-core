namespace Monolith.Core.Domain.Entities;

public sealed record UserMapRoleGroupId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public override string ToString() => base.ToString();
}   
public sealed class UserMapRoleGroup : EntityBase
{
    public UserMapRoleGroupId Id { get; set; }
    public UserId UserId { get; set; }
    public RoleGroupId RoleGroupId { get; set; }

    public User User { get; set; }
    public RoleGroup RoleGroup { get; set; }


}   