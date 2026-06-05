// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Persistence
{
    public record QueryFilter<TObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryFilter{TObject}"/> record, specifying paging, filtering, and ordering criteria.
        /// </summary>
        /// <param name="pageNo">The page number (1-based).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="where">An optional predicate to filter items.</param>
        /// <param name="orderBys">An optional array of key/ascending tuples for sorting.</param>
        /// <param name="materialiserFunc">An optional delegate to fully materialize the query results.</param>
        public QueryFilter(int pageNo = 1, int pageSize = 20, Expression<Func<TObject, bool>> where = null, (Expression<Func<TObject, object>> key, bool asc)[] orderBys = null, Func<IQueryable<TObject>, CancellationToken, Task<IEnumerable<TObject>>> materialiserFunc = null)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            Where = where;
            OrderBys = orderBys;
            MaterialiserFunc = materialiserFunc;
        }

        public int PageNo { get; }

        public int PageSize { get; } 

        public Expression<Func<TObject, bool>> Where { get; }
        
        public (Expression<Func<TObject, object>> key, bool asc)[] OrderBys { get; }
        
        /// <summary>
        /// Optional: The function used to fully materialise the results
        /// </summary>
        public Func<IQueryable<TObject>, CancellationToken, Task<IEnumerable<TObject>>> MaterialiserFunc { get; }
    }
}
