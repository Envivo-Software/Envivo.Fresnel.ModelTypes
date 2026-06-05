// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Envivo.Fresnel.ModelTypes.Infrastructure.Classes
{
    /// <summary>
    /// A JSON serializer that stores data in memory, implementing IJsonObjectSerializer to fulfill architecture structural contracts.
    /// </summary>
    public sealed class InMemoryJsonSerializer : IJsonObjectSerializer
    {
        private readonly JsonSerializerSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryJsonSerializer"/> class.
        /// </summary>
        public InMemoryJsonSerializer()
        {
            _settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,

                // Serialise *all fields* (public + private):
                ContractResolver = new InternalFieldsContractResolver(),

                // Handle polymorphic types:
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,

                // Handle circular references:
                PreserveReferencesHandling = PreserveReferencesHandling.All,
            };
        }

        /// <summary>
        /// Deserializes the specified JSON to a non-generic type.
        /// </summary>
        /// <param name="json">The JSON to deserialize.</param>
        /// <param name="type">The <see cref="Type"/> of the object that the <see cref="Deserialize(string, Type)"/> method returns.</param>
        /// <returns>The deserialized object.</returns>
        public object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, _settings);
        }

        /// <summary>
        /// Deserializes the specified JSON to a generic type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="json">The JSON to deserialize.</param>
        /// <returns>The deserialized object of type T.</returns>
        public T Deserialize<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }

        /// <summary>
        /// Serializes the specified object to a JSON string.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public string Serialize<T>(T obj) where T : class
        {
            return JsonConvert.SerializeObject(obj, _settings);
        }

        /// <summary>
        /// A custom contract resolver that serializes all fields, including private and non-public instance fields, excluding compiler-generated backing fields.
        /// </summary>
        private sealed class InternalFieldsContractResolver : DefaultContractResolver
        {
            protected override List<MemberInfo> GetSerializableMembers(Type objectType)
            {
                var members = base.GetSerializableMembers(objectType);

                // Add instance non-public fields (exclude compiler-generated backing fields)
                const BindingFlags flags =
                    BindingFlags.Instance |
                    BindingFlags.NonPublic |
                    BindingFlags.Public;

                var extraFields =
                    objectType
                    .GetFields(flags)
                    .Where(f => !f.IsDefined(typeof(CompilerGeneratedAttribute), true))
                    .Cast<MemberInfo>();

                // Union by metadata identity to avoid duplicates
                var results =
                    members
                    .Concat(extraFields)
                    .Distinct()
                    .ToList();

                return results;
            }

            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var jProp = base.CreateProperty(member, memberSerialization);

                if (member is FieldInfo)
                {
                    jProp.Readable = true;
                    jProp.Writable = true;
                }
                else if (member is PropertyInfo pi)
                {
                    jProp.Readable = jProp.Readable || pi.GetGetMethod(true) != null;
                    jProp.Writable = jProp.Writable || pi.GetSetMethod(true) != null;
                }

                return jProp;
            }
        }
    }
}
