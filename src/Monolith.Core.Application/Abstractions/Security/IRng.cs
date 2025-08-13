namespace Monolith.Core.Application.Abstractions.Security;
public interface IRng
{
    string Generate(int length = 50, bool removeSpecialChars = true);
}