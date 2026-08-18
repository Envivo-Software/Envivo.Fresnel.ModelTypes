// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Persistence.Classes
{
    /// <summary>
    /// The context within which a QueryFilter is executed
    /// </summary>
    /// <remarks>
    /// This is designed to be serialisable, hence the primitive types
    /// </remarks>
    public record QueryFilterContextDTO
    {
        /// <summary>
        /// The type of the Class being searched for
        /// </summary>
        public string ClassTypeName { get; init; }

        /// <summary>
        /// The parent object (if applicable)
        /// </summary>
        public Guid ObjectId { get; init; }

        /// <summary>
        /// The name of the QuerySpecification to be executed
        /// </summary>
        public string QuerySpecificationTypeName { get; init; }

        /// <summary>
        /// The name of the Property being searched against (if applicable)
        /// </summary>
        public string PropertyName { get; init; }

        /// <summary>
        /// The name of the Parameter being searched against (if applicable)
        /// </summary>
        public string ParameterName { get; init; }

        /// <summary>
        /// The name of the Method that the ParameterName belongs to (if applicable)
        /// </summary>
        public string MethodName { get; init; }
    }
}
