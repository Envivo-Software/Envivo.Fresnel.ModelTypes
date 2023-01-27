// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Used for recording basic audit information for a persisted Domain Object
    /// </summary>
    public interface IAudit
    {
        /// <summary>
        /// The Domain Object this information is for
        /// </summary>
        IDomainObject DomainObject { get; set; }

        /// <summary>
        /// The user that created the associated object
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// The date/time the associated object was created
        /// </summary>
        DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The user that updated the associated object (if an update took place)
        /// </summary>
        string UpdatedBy { get; set; }

        /// <summary>
        /// The date/time the associated object was updated (if an update took place)
        /// </summary>
        DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// The user that deleted the associated object (if a delete took place)
        /// </summary>
        string DeletedBy { get; set; }

        /// <summary>
        /// The date/time the associated object was deleted (if a delete took place)
        /// </summary>
        DateTime? DeletedAt { get; set; }
    }
}