using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace App.Serializers
{
    public class MemoryJsonSerializer<T> : AbstractJsonSerializer<T>, IMemoryJsonSerializer<T>
    {
        public MemoryJsonSerializer(ILogger logger) : base(logger)
        {
        }

        public string Serialize(T obj)
        {
            try
            {
                using var stream = new MemoryStream();
                Serializer.WriteObject(stream, obj);
                var bytes = stream.ToArray();
                var json = Encoding.UTF8.GetString(bytes);
                return json;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to serialize object {@obj}", obj);
            }

            return null;
        }

        public T Deserialize(string json)
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(json);
                using var stream = new MemoryStream(bytes);
                var obj = (T)Serializer.ReadObject(stream);
                return obj;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to deserialize json {data}", json);
            }

            return default(T);
        }
    }
}