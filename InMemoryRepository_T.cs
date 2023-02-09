// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Envivo.Fresnel.ModelTypes
{
    /// <summary>
    /// A simple repository that keeps it's contents in-memory.
    /// Used when designing domain models.
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public class InMemoryRepository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        private readonly Dictionary<Guid, JsonEntry> _Items = new();

        private readonly JsonSerializerOptions _JsonSerializerOptions =
            new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                IncludeFields = true,
                WriteIndented = true,
            };

        public InMemoryRepository()
        {
        }

        public InMemoryRepository(IEnumerable<TAggregateRoot> initialItems)
        {
            foreach (var obj in initialItems)
            {
                _Items[obj.Id] = CreateJsonEntry(obj);
            }
        }

        public IQueryable<TAggregateRoot> GetAll()
        {
            var results =
                _Items.Values
                .Select(o => JsonSerializer.Deserialize(o.Json, o.Type, _JsonSerializerOptions))
                .Cast<TAggregateRoot>()
                .ToList();
            return results.AsQueryable();
        }

        public TAggregateRoot? Load(Guid id)
        {
            var match = _Items.GetValueOrDefault(id);
            return
                match == null ?
                null :
                JsonSerializer.Deserialize(match.Json, match.Type, _JsonSerializerOptions) as TAggregateRoot;
        }

        public int Save(TAggregateRoot aggregateRoot, IEnumerable<object> newObjects, IEnumerable<object> modifiedObjects, IEnumerable<object> deletedObjects)
        {
            var newAggregates =
                newObjects
                .OfType<TAggregateRoot>()
                .ToList();
            foreach (var ar in newAggregates)
            {
                Save(ar);
            }

            var modifiedAggregates =
                modifiedObjects.
                OfType<TAggregateRoot>()
                .ToList();
            foreach (var ar in modifiedAggregates)
            {
                Save(ar);
            }

            return newAggregates.Count() + modifiedAggregates.Count();
        }

        private void Save(TAggregateRoot aggregateRoot)
        {
            _Items[aggregateRoot.Id] = CreateJsonEntry(aggregateRoot);
        }

        public void Delete(TAggregateRoot aggregateRoot)
        {
            _Items.Remove(aggregateRoot.Id);
        }

        public IAggregateLock Lock(TAggregateRoot aggregateRoot)
        {
            // Not applicable
            return null;
        }

        public void Unlock(TAggregateRoot aggregateRoot)
        {
            // Not applicable
        }

        private JsonEntry CreateJsonEntry(TAggregateRoot obj)
        {
            return new JsonEntry
            {
                Id = obj.Id,
                Type = obj.GetType(),
                Json = JsonSerializer.Serialize(obj, _JsonSerializerOptions)
            };
        }

        private record JsonEntry
        {
            internal Guid Id { get; set; }

            internal Type Type { get; set; }

            internal string Json { get; set; }
        }
    }
}