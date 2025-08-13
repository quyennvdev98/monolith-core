using System.Text.Encodings.Web;
using Monolith.Core.Application.Abstractions.Security;
using Monolith.Core.Domain.Enums;


namespace Monolith.Core.Infrastructure.Security;

public sealed class SecurityProvider : ISecurityProvider
{
    private readonly IHasher _hasher;
    private readonly IRng _rng;
    private readonly UrlEncoder _urlEncoder;
    private readonly IReadOnlyDictionary<AlgorithmNameType, IEncryptionStrategy> _encryptionStrategies;
    private readonly IReadOnlyDictionary<AlgorithmNameType, ISigningStrategy> _signingStrategies;


    public SecurityProvider(
        IEnumerable<IEncryptionStrategy> encryptionStrategies,
        IEnumerable<ISigningStrategy> signingStrategies,
        IHasher hasher,
        IRng rng,
        UrlEncoder urlEncoder)
    {
        _hasher = hasher;
        _rng = rng;
        _urlEncoder = urlEncoder;
        _encryptionStrategies = encryptionStrategies.ToDictionary(x => x.AlgorithmType);
        _signingStrategies = signingStrategies.ToDictionary(x => x.AlgorithmType);
    }

    private IEncryptionStrategy GetEncryptionStrategy(AlgorithmNameType algorithm)
    {
        if (_encryptionStrategies.TryGetValue(algorithm, out var strategy))
            return strategy;

        throw new NotSupportedException($"Encryption algorithm '{algorithm}' is not supported.");
    }

    private ISigningStrategy GetSigningStrategy(AlgorithmNameType algorithm)
    {
        if (_signingStrategies.TryGetValue(algorithm, out var strategy))
            return strategy;

        throw new NotSupportedException($"Signing algorithm '{algorithm}' is not supported.");
    }

    public string Encrypt(string data, string key, AlgorithmNameType algorithm)
    {

        var strategy = GetEncryptionStrategy(algorithm);
        return strategy.Encrypt(data, key);
    }

    public string Decrypt(string data, string key, AlgorithmNameType algorithm)
    {

        var strategy = GetEncryptionStrategy(algorithm);
        return strategy.Decrypt(data, key);
    }

    public string Sign(string data, AlgorithmNameType algorithm, string privateKey)
    {
        var strategy = GetSigningStrategy(algorithm);
        return strategy.Sign(data, privateKey);
    }

    public bool Verify(string data, string signature, AlgorithmNameType algorithm, string publicKey)
    {
        var strategy = GetSigningStrategy(algorithm);
        return strategy.Verify(data, signature, publicKey);
    }

    public string Hash(string data) => _hasher.Hash(data);

    public string Rng(int length = 50, bool removeSpecialChars = true)
        => _rng.Generate(length, removeSpecialChars);

    public string Sanitize(string value)
        => _urlEncoder.Encode(value);
}
