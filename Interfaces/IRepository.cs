// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Interfaces
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
        /// Returns a queryable of the Objects for this repository. This query is extended at run-time, prior to the results being materialised.
        /// </summary>
        /// <returns>An IQueryable from the underlying database provider</returns>
        [Obsolete("Use GetResultsAsync() instead, which does not leak IQueryable into domain code")]
        IQueryable<TObject> GetQuery();

        /// <summary>
        /// Returns the Object matches for the given query expression and ordering.
        /// </summary>
        /// <param name="where">The Where filter expression</param>
        /// <param name="orderBys">Optiona: OrderBy expressions</param>
        /// <param name="toListAsync">Optional: The action to materialise the list</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The list of matches from the underlying database provider</returns>
        async Task<IEnumerable<TObject>> GetResultsAsync(
            Expression<Func<TObject, bool>> where,
            (Expression<Func<TObject, object>> key, bool asc)[] orderBys = null,
            Func<IQueryable<TObject>, CancellationToken, Task<IEnumerable<TObject>>> toListAsync = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(where);

#pragma warning disable CS0618 // Type or member is obsolete
            // This is acceptable, as we're not leaking the IQueryable into consumer code:
            var query = GetQuery();
#pragma warning restore CS0618 // Type or member is obsolete

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderBys.Length > 0)
            {
                var ordered = orderBys[0].asc
                    ? query.OrderBy(orderBys[0].key)
                    : query.OrderByDescending(orderBys[0].key);

                for (int i = 1; i < orderBys.Length; i++)
                {
                    ordered = orderBys[i].asc
                        ? ordered.ThenBy(orderBys[i].key)
                        : ordered.ThenByDescending(orderBys[i].key);
                }

                query = ordered;
            }

            if (toListAsync != null)
            {
                return await toListAsync(query, cancellationToken);
            }

            // Default to synchronous:
            return query.ToList();
        }

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
