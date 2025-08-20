using System.ComponentModel;

namespace Monolith.Core.Domain;

[TypeConverter(typeof(StronglyTypedIdConverter))]
public record StronglyTypedId<TValue>(TValue Value)  where TValue : notnull
{
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();
}