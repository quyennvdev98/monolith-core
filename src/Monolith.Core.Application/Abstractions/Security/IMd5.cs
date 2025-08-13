namespace Monolith.Core.Application.Abstractions.Security;
public interface IMd5
{
    string Calculate(string value);
}