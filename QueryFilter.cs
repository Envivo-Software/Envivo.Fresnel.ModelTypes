// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace Envivo.Fresnel.ModelTypes
{
    /// <inheritdoc cref="IQueryFilter" />
    public class QueryFilter : IQueryFilter
    {
        /// <inheritdoc/>
        public string ClassTypeName { get; set; }

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