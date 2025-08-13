namespace Monolith.Core.Application.Abstractions.Security;
public interface IHasher
{
    string Hash(string data);
}