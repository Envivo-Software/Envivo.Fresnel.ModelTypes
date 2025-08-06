// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IJsonObjectSerializer _JsonObjectSerializer;
        private readonly Dictionary<Guid, JsonEntry> _Items = new();

        public InMemoryRepository(IJsonObjectSerializer jsonObjectSerializer = null)
        {
            _JsonObjectSerializer = jsonObjectSerializer ?? new InMemoryJsonSerializer();
        }

        public InMemoryRepository(IEnumerable<TAggregateRoot> initialItems, IJsonObjectSerializer jsonObjectSerializer = null)
            : this(jsonObjectSerializer)
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
            ArgumentNullException.ThrowIfNull(_JsonObjectSerializer, nameof(_JsonObjectSerializer));

            var json = _JsonObjectSerializer.Serialize(obj);

            return new JsonEntry
            {
                Id = obj.Id,
                Type = obj.GetType(),
                Json = json
            };
        }

        private TAggregateRoot Deserialise(JsonEntry entry)
        {
            ArgumentNullException.ThrowIfNull(_JsonObjectSerializer, nameof(_JsonObjectSerializer));

            if (entry == null)
                return null;

            return _JsonObjectSerializer.Deserialize<TAggregateRoot>(entry.Json);
        }

        private record JsonEntry
        {
            internal Guid Id { get; set; }

            internal Type Type { get; set; }

            internal string Json { get; set; }
        }
    }
}
