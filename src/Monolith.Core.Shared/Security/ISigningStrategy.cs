using Monolith.Core.Domain.Enums;

namespace Monolith.Core.Shared.Security;

public interface ISigningStrategy
{
    AlgorithmNameType AlgorithmType { get; }

    string Sign(string data, string privateKeyBase64);
    bool Verify(string data, string signatureBase64, string publicKeyBase64);
}