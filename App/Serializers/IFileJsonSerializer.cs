namespace App.Serializers
{
    public interface IFileJsonSerializer<T>
    {
        void Serialize(T obj);
        T Deserialize();
    }
}