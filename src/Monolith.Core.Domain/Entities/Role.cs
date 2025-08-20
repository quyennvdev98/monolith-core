namespace Monolith.Core.Domain.Entities;

public sealed record RoleId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public override string ToString() => base.ToString();
}   
public sealed class Role : EntityBase
{
    public RoleId Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public string Description { get; set; }
    public bool IsActivated { get; set; }
    public bool IsRemoved { get; set; }
    public UserId CreatorId { get; set; }
    public UserId UpdaterId { get; set; }


    public User Creator { get; set; }
    public User Updater { get; set; }
    public List<PolicyMapRole> PolicyMapRoles { get; set; }
}