using NSec.Cryptography;

namespace Monolith.Core.Infrastructure.Security;

public class Ed25519KeyGenerator
{
    public static (string PrivateKey, string PublicKey) Generate()
    {
        var algorithm = SignatureAlgorithm.Ed25519;
        var key = Key.Create(algorithm);

        string privateKey = Convert.ToBase64String(key.Export(KeyBlobFormat.RawPrivateKey));
        string publicKey = Convert.ToBase64String(key.Export(KeyBlobFormat.RawPublicKey));

        return (privateKey, publicKey);
    }
}