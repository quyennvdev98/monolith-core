namespace Monolith.Core.Application.Abstractions;

public interface ISerializer
{
    string Serialize(object obj);
    TObject? Deserialize<TObject>(string json);
    object Deserialize(string json, Type obj);
    byte[] SerializeBinary(object @object);
    TObject? DeserializeBinary<TObject>(byte[] objectBytes);
}
