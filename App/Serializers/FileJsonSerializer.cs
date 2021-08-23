using System;
using System.IO;
using App.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace App.Serializers
{
    public class FileJsonSerializer<T> : AbstractJsonSerializer<T>, IFileJsonSerializer<T>
    {
        protected string FileName => Options.Value.FileName;

        public FileJsonSerializer(IOptions<Settings> options, ILogger logger) : base(options, logger)
        {
        }

        public void Serialize(T obj)
        {
            try
            {
                using var stream = new FileStream(FileName, FileMode.Create);
                Serializer.WriteObject(stream, obj);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to serialize object {@obj}", obj);
            }
        }

        public T Deserialize()
        {
            try
            {
                using var stream = new FileStream(FileName, FileMode.Open);
                var obj = (T) Serializer.ReadObject(stream);
                return obj;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to deserialize json {data}", FileName);
            }

            return default(T);
        }
    }
}