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
        
        public Func<IQueryable<TObject>, CancellationToken, Task<IEnumerable<TObject>>> MaterialiserFunc { get; }
    }
}
