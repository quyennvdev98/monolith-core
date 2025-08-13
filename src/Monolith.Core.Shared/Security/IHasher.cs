namespace Monolith.Core.Shared.Security;
public interface IHasher
{
    string Hash(string data);
}