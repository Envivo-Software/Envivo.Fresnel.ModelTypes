// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Envivo.Fresnel.ModelTypes.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Envivo.Fresnel.ModelTypes
{
    public sealed class InMemoryJsonSerializer : IJsonObjectSerializer
    {
        private readonly JsonSerializerSettings _settings;

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

        public object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, _settings);
        }

        public T Deserialize<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }

        public string Serialize<T>(T obj) where T : class
        {
            return JsonConvert.SerializeObject(obj, _settings);
        }

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
