// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Envivo.Fresnel.ModelTypes
{
    /// <summary>
    /// Used for recording basic audit information for a persisted Domain Object
    /// </summary>
    public class Audit : IAudit
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual Guid ParentObjectId { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual DateTime? CreatedAt { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual string UpdatedBy { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual string DeletedBy { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual DateTime? DeletedAt { get; set; }
    }
}