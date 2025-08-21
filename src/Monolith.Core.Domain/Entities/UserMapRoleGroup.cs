namespace Monolith.Core.Domain.Entities;


public sealed class UserMapRoleGroup : EntityBase
{
   
    public UserId UserId { get; set; }
    public RoleGroupId RoleGroupId { get; set; }

    public User User { get; set; }
    public RoleGroup RoleGroup { get; set; }


}   