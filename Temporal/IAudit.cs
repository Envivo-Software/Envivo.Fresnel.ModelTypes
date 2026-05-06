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
        Guid ParentObjectId { get; set; }

        /// <summary>
        /// The user that created the associated object
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// The date/time the associated object was created
        /// </summary>
        DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// The user that updated the associated object (if an update took place)
        /// </summary>
        string UpdatedBy { get; set; }

        /// <summary>
        /// The date/time the associated object was updated (if an update took place)
        /// </summary>
        DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// The user that deleted the associated object (if a delete took place)
        /// </summary>
        string DeletedBy { get; set; }

        /// <summary>
        /// The date/time the associated object was deleted (if a delete took place)
        /// </summary>
        DateTimeOffset? DeletedAt { get; set; }
    }
}