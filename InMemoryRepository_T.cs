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
        private readonly Dictionary<Guid, string> _Items = new();

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
                _Items[obj.Id] = JsonSerializer.Serialize(obj, _JsonSerializerOptions);
            }
        }

        public IQueryable<TAggregateRoot> GetAll()
        {
            var results =
                _Items.Values
                .Select(o => JsonSerializer.Deserialize<TAggregateRoot>(o, _JsonSerializerOptions))
                .ToList();
            return results.AsQueryable();
        }

        public TAggregateRoot? Load(Guid id)
        {
            var match = _Items.GetValueOrDefault(id);
            return
                match == null ?
                null :
                JsonSerializer.Deserialize<TAggregateRoot>(match, _JsonSerializerOptions);
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
            _Items[aggregateRoot.Id] = JsonSerializer.Serialize(aggregateRoot, _JsonSerializerOptions);
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
    }
}