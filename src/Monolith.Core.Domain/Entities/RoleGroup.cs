namespace Monolith.Core.Domain.Entities;
public sealed record RoleGroupId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public override string ToString() => base.ToString();
}
public sealed class RoleGroup : EntityBase
{
    public RoleGroupId Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SearchHint { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActivated { get; set; }
    public bool IsRemoved { get; set; }
    public UserId CreatorId { get; set; }
    public UserId UpdaterId { get; set; }
    
    public User Creator { get; set; }
    public User Updater { get; set; }
    public List<RoleGroupMapRole> RoleGroupMapRoles { get; set; }
    public List<UserMapRoleGroup> UserMapRoleGroups { get; set; }
    public List<RoleGroupClaim> RoleGroupClaims { get; set; }
}
