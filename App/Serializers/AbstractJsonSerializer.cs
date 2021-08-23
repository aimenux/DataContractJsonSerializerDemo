using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using App.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace App.Serializers
{
    public class AbstractJsonSerializer<T>
    {
        protected readonly DataContractJsonSerializer Serializer;
        protected readonly IOptions<Settings> Options;
        protected readonly ILogger Logger;

        protected AbstractJsonSerializer(ILogger logger)
        {
            Logger = logger;
            var settings = new DataContractJsonSerializerSettings
            {
                EmitTypeInformation = EmitTypeInformation.Always,
                KnownTypes = GetKnownTypes()
            };
            Serializer = new DataContractJsonSerializer(typeof(T), settings);
        }

        protected AbstractJsonSerializer(IOptions<Settings> options, ILogger logger) : this(logger)
        {
            Options = options;
        }

        protected static IEnumerable<Type> GetKnownTypes()
        {
            var currentTypes = typeof(T)
                .Assembly
                .GetTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsSerializable);

            var propertyTypes = typeof(T)
                .GetProperties()
                .Select(p => p.PropertyType)
                .Where(t => t.IsClass)
                .Where(t => t.IsSerializable)
                .Where(t => currentTypes.Contains(t));

            var knownTypes = new List<Type>(propertyTypes)
            {
                typeof(T)
            };

            return knownTypes;
        }
    }
}