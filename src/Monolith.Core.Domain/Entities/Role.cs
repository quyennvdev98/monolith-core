namespace Monolith.Core.Domain.Entities;

public sealed class Role : Entity<Guid>
{
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public string Description { get; set; }
    public bool IsActivated { get; set; }
    public bool IsRemoved { get; set; }
    public Guid CreatorId { get; set; }
    public User Creator { get; set; }
    public Guid UpdaterId { get; set; }
    public User Updater { get; set; }
    public List<PolicyMapRole> PolicyMapRoles { get; set; }
}