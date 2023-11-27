// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
                _Items.Add(obj.Id, CreateJsonEntry(obj));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public IQueryable<TAggregateRoot> GetQuery()
        {
            return
                _Items.Values
                .Select(entry => Deserialise(entry))
                .Where(e => e != null)
                .AsQueryable();
        }

        public async Task<TAggregateRoot> LoadAsync(Guid id)
        {
            var match = _Items.GetValueOrDefault(id);
            var result =
                match == null ?
                null :
                Deserialise(match);

            return await Task.FromResult(result);
        }

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
                modifiedObjects.
                OfType<TAggregateRoot>()
                .ToList();
            foreach (var ar in modifiedAggregates)
            {
                Save(ar);
            }

            var result = newAggregates.Count + modifiedAggregates.Count;
            return await Task.FromResult(result);
        }

        public async Task DeleteAsync(TAggregateRoot aggregateRoot)
        {
            _Items.Remove(aggregateRoot.Id);
            await Task.CompletedTask;
        }

        public async Task<IAggregateLock> LockAsync(TAggregateRoot aggregateRoot)
        {
            var result = (IAggregateLock)null;
            return await Task.FromResult(result);
        }

        public async Task UnlockAsync(TAggregateRoot aggregateRoot)
        {
            await Task.CompletedTask;
        }

        private void Save(TAggregateRoot aggregateRoot)
        {
            _Items[aggregateRoot.Id] = CreateJsonEntry(aggregateRoot);
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
    }
}