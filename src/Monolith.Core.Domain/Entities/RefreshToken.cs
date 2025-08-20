namespace Monolith.Core.Domain.Entities;

public sealed record RefreshTokenId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public override string ToString() => base.ToString();
}

public sealed class RefreshToken : EntityBase
{
    public RefreshTokenId Id { get; set; }
    public string UserToken { get; set; }
    public string JwtId { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime TokenExpiryTime { get; set; }
    public bool IsUsed { get; set; }
    public bool Invalidate { get; set; }
    public UserId UserId { get; set; }
    
    public User User { get; set; }


}