

using Monolith.Core.Domain.Enums;

namespace Monolith.Core.Shared.Security;
public interface ISecurityProvider
{
    string Encrypt(string data, string key, AlgorithmNameType  algorithm);
    string Decrypt(string data, string key, AlgorithmNameType  algorithm );
    string Sign(string data, AlgorithmNameType algorithm, string privateKey);
    bool Verify(string data, string signature, AlgorithmNameType algorithm, string publicKey);
    string Hash(string data);
    string Rng(int length = 50, bool removeSpecialChars = true);
    string Sanitize(string value);
}