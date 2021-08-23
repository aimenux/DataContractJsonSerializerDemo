namespace App.Serializers
{
    public interface IMemoryJsonSerializer<T>
    {
        string Serialize(T obj);
        T Deserialize(string json);
    }
}