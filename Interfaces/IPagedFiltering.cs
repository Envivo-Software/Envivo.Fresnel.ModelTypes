// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Used to query for a page of items from a Persistence Store.
    /// Implement this if the underlying database provider doesn't fully support IQueryable.
    /// </summary>
    public interface IPagedFiltering<T>
        where T : class
    {
        /// <summary>
        /// Returns a subset of items using the given query filter. 
        /// </summary>
        /// <param name="queryFilter">The query filter to apply</param>
        /// <returns>The matching items, along with the available count from the persistence store</returns>
        Task<(IEnumerable<T>, int)> GetResultsPageAsync(IQueryFilter queryFilter);
    }
}