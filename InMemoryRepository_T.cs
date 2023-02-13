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

        public async Task<IEnumerable<TAggregateRoot>> FindAsync(Predicate<TAggregateRoot> predicate, int pageNumber, int pageSize, string orderBy)
        {
            var results =
                _Items.Values
                .Select(o => JsonSerializer.Deserialize(o.Json, o.Type, _JsonSerializerOptions))
                .Cast<TAggregateRoot>()
                .ToList();

            if (predicate != null)
            {
                results =
                    results.Where(m => predicate(m))
                    .ToList();
            }

            // TODO: Add OrderBy

            results =
                results
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return await Task.FromResult(results);
        }

        public async Task<TAggregateRoot> LoadAsync(Guid id)
        {
            var match = _Items.GetValueOrDefault(id);
            var result =
                match == null ?
                null :
                JsonSerializer.Deserialize(match.Json, match.Type, _JsonSerializerOptions) as TAggregateRoot;

            return await Task.FromResult(result);
        }

        public async Task<int> SaveAsync(TAggregateRoot aggregateRoot, IEnumerable<object> newObjects, IEnumerable<object> modifiedObjects, IEnumerable<object> deletedObjects)
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
            await Task.CompletedTask;
            return null;
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

        private record JsonEntry
        {
            internal Guid Id { get; set; }

            internal Type Type { get; set; }

            internal string Json { get; set; }
        }
    }
}