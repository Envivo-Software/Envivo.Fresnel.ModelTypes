// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using Envivo.Fresnel.ModelTypes.Interfaces;
using Newtonsoft.Json;

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
    }
}
