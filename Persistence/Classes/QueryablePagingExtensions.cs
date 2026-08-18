// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Persistence.Classes
{
    public static class QueryablePagingExtensions
    {
        /// <summary>
        /// Applies the given filter, including Where clauses, ordering, and paging, then returns the resulting <see cref="QueryResult{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the query items.</typeparam>
        /// <param name="query">The source query to apply filters to.</param>
        /// <param name="queryFilter">The filter containing Where clauses, OrderBy specifications, and paging information.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="QueryResult{T}"/> containing the filtered, ordered, and paged results.</returns>
        public static async Task<QueryResult<T>> GetResultsAsync<T>(
            this IQueryable<T> query,
            QueryFilter<T> queryFilter,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(query);
            ArgumentNullException.ThrowIfNull(queryFilter);

            // Apply filtering and sorting
            var filteredQuery =
                queryFilter.Where == null ?
                query :
                query.Where(queryFilter.Where);

            if (queryFilter.OrderBys?.Length > 0)
            {
                var firstOrderBy = queryFilter.OrderBys[0];
                var orderedQuery = firstOrderBy.asc
                    ? filteredQuery.OrderBy(firstOrderBy.key)
                    : filteredQuery.OrderByDescending(firstOrderBy.key);

                for (int i = 1; i < queryFilter.OrderBys.Length; i++)
                {
                    orderedQuery = queryFilter.OrderBys[i].asc
                        ? orderedQuery.ThenBy(queryFilter.OrderBys[i].key)
                        : orderedQuery.ThenByDescending(queryFilter.OrderBys[i].key);
                }

                filteredQuery = orderedQuery;
            }

            var totalItems = filteredQuery.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / queryFilter.PageSize);

            // Apply paging
            var pagedQuery =
                filteredQuery
                .Skip((queryFilter.PageNo - 1) * queryFilter.PageSize)
                .Take(queryFilter.PageSize);

            var items =
                queryFilter.MaterialiserFunc != null ?
                await queryFilter.MaterialiserFunc(pagedQuery, cancellationToken) :
                pagedQuery.ToList();

            return new QueryResult<T>(items, queryFilter.PageNo, queryFilter.PageSize, totalItems, totalPages);
        }
    }
}
