// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Structural;
using System;

namespace Envivo.Fresnel.ModelTypes.Temporal
{
    /// <summary>
    /// Used for recording basic audit information for a persisted Domain Object
    /// </summary>
    public interface IAudit : IValueObject
    {
        /// <summary>
        /// A reference to the associated object
        /// </summary>
        Guid ParentObjectId { get; init; }

        /// <summary>
        /// The user that created the associated object
        /// </summary>
        string CreatedBy { get; init; }

        /// <summary>
        /// The date/time the associated object was created
        /// </summary>
        DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// The user that updated the associated object (if an update took place)
        /// </summary>
        string UpdatedBy { get; init; }

        /// <summary>
        /// The date/time the associated object was updated (if an update took place)
        /// </summary>
        DateTimeOffset? UpdatedAt { get; init; }

        /// <summary>
        /// The user that deleted the associated object (if a delete took place)
        /// </summary>
        string DeletedBy { get; init; }

        /// <summary>
        /// The date/time the associated object was deleted (if a delete took place)
        /// </summary>
        DateTimeOffset? DeletedAt { get; init; }
    }
}