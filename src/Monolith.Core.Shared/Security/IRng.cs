namespace Monolith.Core.Shared.Security;
public interface IRng
{
    string Generate(int length = 50, bool removeSpecialChars = true);
}