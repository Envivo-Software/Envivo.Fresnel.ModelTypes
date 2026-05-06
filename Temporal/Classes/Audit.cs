// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.ComponentModel.DataAnnotations;

namespace Envivo.Fresnel.ModelTypes.Temporal.Classes
{
    /// <inheritdoc cref="IAudit" />
    public class Audit : IAudit
    {
        /// <inheritdoc/>
        [Key]
        public Guid Id { get; set; }

        /// <inheritdoc/>
        public virtual Guid ParentObjectId { get; set; }

        /// <inheritdoc/>
        public virtual string CreatedBy { get; set; }

        /// <inheritdoc/>
        public virtual DateTimeOffset? CreatedAt { get; set; }

        /// <inheritdoc/>
        public virtual string UpdatedBy { get; set; }

        /// <inheritdoc/>
        public virtual DateTimeOffset? UpdatedAt { get; set; }

        /// <inheritdoc/>
        public virtual string DeletedBy { get; set; }

        /// <inheritdoc/>
        public virtual DateTimeOffset? DeletedAt { get; set; }
    }
}