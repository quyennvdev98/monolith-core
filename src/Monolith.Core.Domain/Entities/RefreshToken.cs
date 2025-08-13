namespace Monolith.Core.Domain.Entities;

public sealed class RefreshToken : Entity<Guid>
{
    public string UserToken { get; set; }
    public string JwtId { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime TokenExpiryTime { get; set; }
    public bool IsUsed { get; set; }
    public bool Invalidate { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    
}