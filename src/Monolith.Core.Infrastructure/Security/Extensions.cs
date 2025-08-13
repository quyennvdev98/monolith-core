

using Microsoft.Extensions.DependencyInjection;
using Monolith.Core.Infrastructure.Security.Encryption;
using Monolith.Core.Shared.Security;

namespace Monolith.Core.Infrastructure.Security;

public static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
        =>  services
            .AddSingleton<ISecurityProvider, SecurityProvider>()
            .AddSingleton<IHasher, Hasher>()
            .AddSingleton<IMd5, Md5>()
            .AddSingleton<IRng, Rng>()
            .AddEncryptionStrategies()
            .AddSigningStrategies();
    

    private static IServiceCollection AddEncryptionStrategies(this IServiceCollection services)
       => services
            .AddSingleton<IEncryptionStrategy, AESEncryptor>()
            .AddSingleton<IEncryptionStrategy, RSAEncryptor>();
    

    private static IServiceCollection AddSigningStrategies(this IServiceCollection services)
        => services
            .AddSingleton<ISigningStrategy, Ed25519Signing>();
    
            
}

