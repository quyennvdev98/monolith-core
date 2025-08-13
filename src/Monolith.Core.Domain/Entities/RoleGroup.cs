namespace Monolith.Core.Domain.Entities;

public sealed class RoleGroup : Entity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string SearchHint { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActivated { get; set; }
    public bool IsRemoved { get; set; }
    public Guid CreatorId { get; set; }
    public User Creator { get; set; }
    public Guid UpdaterId { get; set; }
    public User Updater { get; set; }

    public List<RoleGroupMapRole> RoleGroupMapRoles { get; set; }
    public List<UserMapRoleGroup> UserMapRoleGroups { get; set; }
    public List<RoleGroupClaim> RoleGroupClaims { get; set; }
}
