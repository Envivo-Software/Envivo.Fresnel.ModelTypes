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
        private readonly List<TAggregateRoot> _Items = new();

        public InMemoryRepository()
        {
        }

        public InMemoryRepository(IEnumerable<TAggregateRoot> initialItems)
        {
            _Items.AddRange(initialItems);
        }

        public IQueryable<TAggregateRoot> GetAll()
        {
            var clones = CreateClone(_Items);
            return clones.AsQueryable();
        }

        public TAggregateRoot? Load(Guid id)
        {
            var match = _Items.FirstOrDefault(p => p.Id == id);
            return
                match == null ?
                null :
                CreateClone(match);
        }

        public int Save(TAggregateRoot aggregateRoot, IEnumerable<object> newObjects, IEnumerable<object> modifiedObjects, IEnumerable<object> deletedObjects)
        {
            var newAggregates = newObjects.OfType<TAggregateRoot>();
            foreach (var ar in newAggregates)
            {
                Save(ar);
            }

            var modifiedAggregates = modifiedObjects.OfType<TAggregateRoot>();
            foreach (var ar in modifiedAggregates)
            {
                Save(ar);
            }

            return newAggregates.Count() + modifiedAggregates.Count();
        }

        private void Save(TAggregateRoot aggregateRoot)
        {
            Delete(aggregateRoot);

            var clone = CreateClone(aggregateRoot);
            _Items.Add(clone);
        }

        public void Delete(TAggregateRoot aggregateRoot)
        {
            var match = _Items.FirstOrDefault(p => p.Id == aggregateRoot.Id);
            if (match != null)
            {
                _Items.Remove(match);
            }
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

        private static T CreateClone<T>(T obj)
        {
            var settings = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            };
            var json = JsonSerializer.Serialize(obj, typeof(T), settings);
            var clone = JsonSerializer.Deserialize<T>(json);
            return clone;
        }
    }
}