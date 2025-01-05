// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IRepository : IDomainDependency
    { }

    /// <summary>
    /// Used to load/save Aggregates of the given type to a Persistence Store.
    /// </summary>
    public interface IRepository<TAggregateRoot> : IRepository
        where TAggregateRoot : class
    {
        /// <summary>
        /// Returns a queryable of the Aggregate Roots for this repository. This query is extended at run-time, prior to the results being materialised.
        /// </summary>
        /// <returns>An IQueryable from the underlying database provider</returns>
        IQueryable<TAggregateRoot> GetQuery();

        /// <summary>
        /// Loads and returns the Aggregate Root matching the given Id
        /// </summary>
        /// <param name="id"></param>
        Task<TAggregateRoot?> LoadAsync(Guid id);

        /// <summary>
        /// Saves the given Aggregate Root and its contents within a transaction.
        /// </summary>
        /// <param name="aggregateRoot">The primary aggregate being saved</param>
        /// <param name="newObjects">Any new objects within the aggregate</param>
        /// <param name="modifiedObjects">Any modified objects within the aggregate</param>
        /// <param name="deletedObjects">Any objects deleted from the aggregate</param>
        /// <returns>The number of saved items</returns>

        Task<int> SaveAsync(TAggregateRoot aggregateRoot, IEnumerable<object> newObjects, IEnumerable<object> modifiedObjects, IEnumerable<object> deletedObjects);

        /// <summary>
        /// Deletes the given Aggregate Root, along with its contents
        /// </summary>
        /// <param name="aggregateRoot"></param>
        Task DeleteAsync(TAggregateRoot aggregateRoot);
    }
}