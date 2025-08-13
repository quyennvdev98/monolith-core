using System.Text;
using Monolith.Core.Application.Abstractions.Security;
using Monolith.Core.Domain.Enums;
using NSec.Cryptography;

namespace Monolith.Core.Infrastructure.Security.Encryption;

public class Ed25519Signing : ISigningStrategy
{
    public AlgorithmNameType AlgorithmType => AlgorithmNameType.Ed25519;

    private static readonly SignatureAlgorithm Algorithm = SignatureAlgorithm.Ed25519;

    public string Sign(string data, string privateKeyBase64)
    {
        var privateKeyBytes = Convert.FromBase64String(privateKeyBase64);
        var key = Key.Import(Algorithm, privateKeyBytes, KeyBlobFormat.RawPrivateKey);
        var signature = Algorithm.Sign(key, Encoding.UTF8.GetBytes(data));

        return Convert.ToBase64String(signature);
    }

    public bool Verify(string data, string signatureBase64, string publicKeyBase64)
    {
        var publicKeyBytes = Convert.FromBase64String(publicKeyBase64);
        var publicKey = PublicKey.Import(Algorithm, publicKeyBytes, KeyBlobFormat.RawPublicKey);

        var signature = Convert.FromBase64String(signatureBase64);
        return Algorithm.Verify(publicKey, Encoding.UTF8.GetBytes(data), signature);
    }
}