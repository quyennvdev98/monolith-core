

using Monolith.Core.Domain.Enums;

namespace Monolith.Core.Shared.Security;

public interface IEncryptionStrategy
{
    AlgorithmNameType AlgorithmType { get; }
    string Encrypt(string data, string key);
    string Decrypt(string data, string key);
}