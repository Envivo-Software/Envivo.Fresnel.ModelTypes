// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Persistence
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IRepository : IDomainDependency
    { }

    /// <summary>
    /// Used to load/save Objects of the given type to a Persistence Store.
    /// </summary>
    public interface IRepository<TObject> : IRepository
        where TObject : class
    {
        /// <summary>
        /// Returns the matches for the given query expression and ordering.
        /// </summary>
        /// <param name="queryFilter">The query filter details</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The list of matches from the underlying database provider</returns>
        Task<QueryResult<TObject>> GetResultsAsync(QueryFilter<TObject> queryFilter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads and returns the Object matching the given Id
        /// </summary>
        /// <param name="id"></param>
        Task<TObject?> LoadAsync(Guid id);

        /// <summary>
        /// Saves the given Object and its contents within a transaction.
        /// </summary>
        /// <param name="mainObject">The primary object being saved</param>
        /// <param name="newObjects">Any new objects within the object graph</param>
        /// <param name="modifiedObjects">Any modified objects within the object graph</param>
        /// <param name="deletedObjects">Any objects deleted from the object graph</param>
        /// <returns>The number of saved items</returns>

        Task<int> SaveAsync(TObject mainObject, IEnumerable<object> newObjects, IEnumerable<object> modifiedObjects, IEnumerable<object> deletedObjects);

        /// <summary>
        /// Deletes the given Object, along with its contents
        /// </summary>
        /// <param name="obj"></param>
        Task DeleteAsync(TObject obj);
    }
}
