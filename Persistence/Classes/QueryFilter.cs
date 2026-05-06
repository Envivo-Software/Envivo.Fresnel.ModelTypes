// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Persistence.Classes
{
    /// <inheritdoc cref="IQueryFilter" />
    public class QueryFilter : IQueryFilter
    {
        /// <inheritdoc/>
        public IQueryFilterContext FilterContext { get; set; } = new QueryFilterContext();

        /// <inheritdoc/>
        public string Filter { get; set; }

        /// <inheritdoc/>
        public object[] FilterArgs { get; set; }

        /// <inheritdoc/>
        public string Sort { get; set; }

        /// <inheritdoc/>
        public int? PageNo { get; set; } = 1;

        /// <inheritdoc/>
        public int? PageSize { get; set; } = 20;
    }
}
