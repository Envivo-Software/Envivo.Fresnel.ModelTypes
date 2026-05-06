// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Persistence.Classes
{
    /// <inheritdoc cref="IQueryFilterContext" />
    public record QueryFilterContext : IQueryFilterContext
    {
        /// <inheritdoc/>
        public string ClassTypeName { get; set; }

        /// <inheritdoc/>
        public Guid ObjectId { get; set; }

        /// <inheritdoc/>
        public string QuerySpecificationTypeName { get; set; }

        /// <inheritdoc/>
        public string PropertyName { get; set; }

        /// <inheritdoc/>
        public string MethodName { get; set; }

        /// <inheritdoc/>
        public string ParameterName { get; set; }
    }
}
