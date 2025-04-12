// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace Envivo.Fresnel.ModelTypes
{
    /// <summary>
    /// A simple repository that keeps it's contents in-memory.
    /// Used when designing domain models.
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public class InMemoryRepository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : class, IEntity
    {
        private readonly Dictionary<Guid, JsonEntry> _Items = new();
        private readonly JsonSerializerOptions _JsonSerializerOptions;

        public InMemoryRepository()
        {
            _JsonSerializerOptions = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                IncludeFields = true,
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
            };
            _JsonSerializerOptions.Converters.Add(new ExplicitTypeInfoJsonConverter());
        }

        public InMemoryRepository(IEnumerable<TAggregateRoot> initialItems)
            : this()
        {
            var hasMissingIds = initialItems.Any(i => i.Id == Guid.Empty);
            var hasDuplicateIds = initialItems.GroupBy(i => i.Id).Any(grp => grp.Count() > 1);

            if (hasMissingIds || hasDuplicateIds)
            {
                throw new ArgumentException("All objects must have a unique Id value");
            }

            foreach (var obj in initialItems)
            {
                _Items.Add(obj.Id, CreateJsonEntry(obj));
            }
        }

        /// <inheritdoc/>
        public IQueryable<TAggregateRoot> GetQuery()
        {
            return
                _Items.Values
                .Select(entry => Deserialise(entry))
                .Where(e => e != null)
                .AsQueryable();
        }

        /// <inheritdoc/>
        public async Task<TAggregateRoot> LoadAsync(Guid id)
        {
            var match = _Items.GetValueOrDefault(id);
            var result =
                match == null ?
                null :
                Deserialise(match);

            return await Task.FromResult(result);
        }

        /// <inheritdoc/>
        public async Task<int> SaveAsync(TAggregateRoot aggregateRoot, IEnumerable<object> newObjects, IEnumerable<object> modifiedObjects, IEnumerable<object> deletedObjects)
        {
            var newAggregates =
                newObjects
                .OfType<TAggregateRoot>()
                .ToList();
            newAggregates.Add(aggregateRoot);
            foreach (var ar in newAggregates)
            {
                Save(ar);
            }

            var modifiedAggregates =
                modifiedObjects
                .OfType<TAggregateRoot>()
                .ToList();
            foreach (var ar in modifiedAggregates)
            {
                Save(ar);
            }

            var result = newAggregates.Count + modifiedAggregates.Count;
            return await Task.FromResult(result);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(TAggregateRoot aggregateRoot)
        {
            _Items.Remove(aggregateRoot.Id);
            await Task.CompletedTask;
        }

        private void Save(TAggregateRoot aggregateRoot)
        {
            _Items[aggregateRoot.Id] = CreateJsonEntry(aggregateRoot);
        }

        private JsonEntry CreateJsonEntry(TAggregateRoot obj)
        {
            var json = JsonSerializer.Serialize(obj, _JsonSerializerOptions);

            return new JsonEntry
            {
                Id = obj.Id,
                Type = obj.GetType(),
                Json = json
            };
        }

        private TAggregateRoot Deserialise(JsonEntry entry)
        {
            if (entry == null)
                return null;

            return JsonSerializer.Deserialize(entry.Json, entry.Type, _JsonSerializerOptions) as TAggregateRoot;
        }

        private record JsonEntry
        {
            internal Guid Id { get; set; }

            internal Type Type { get; set; }

            internal string Json { get; set; }
        }

        internal class ExplicitTypeInfoJsonConverter : JsonConverter<object>
        {
            private const string TypePropertyName = "$type";
            private const string DataPropertyName = "$data";

            public override bool CanConvert(Type typeToConvert) =>
                typeToConvert == typeof(object) ||
                typeToConvert.IsInterface;

            public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                using var document = JsonDocument.ParseValue(ref reader);
                var root = document.RootElement;

                if (!root.TryGetProperty(TypePropertyName, out var typeProperty))
                    throw new JsonException("Missing $type property in polymorphic JSON.");

                var typeName = typeProperty.GetString();
                if (typeName is null)
                    throw new JsonException("Invalid $type value in polymorphic JSON.");

                var actualType = Type.GetType(typeName);
                if (actualType is null)
                    throw new JsonException($"Unable to resolve type '{typeName}'.");

                var dataProperty = root.GetProperty(DataPropertyName);
                var json = dataProperty.GetRawText();

                return JsonSerializer.Deserialize(json, actualType, options);
            }

            public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                writer.WriteString(TypePropertyName, value.GetType().AssemblyQualifiedName);
                writer.WritePropertyName(DataPropertyName);
                JsonSerializer.Serialize(writer, value, value.GetType(), options);
                writer.WriteEndObject();
            }
        }
    }
}
