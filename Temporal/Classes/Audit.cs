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
        public Guid Id { get; init; }

        /// <inheritdoc/>
        public virtual Guid ParentObjectId { get; init; }

        /// <inheritdoc/>
        public virtual string CreatedBy { get; init; }

        /// <inheritdoc/>
        public virtual DateTimeOffset? CreatedAt { get; init; }

        /// <inheritdoc/>
        public virtual string UpdatedBy { get; init; }

        /// <inheritdoc/>
        public virtual DateTimeOffset? UpdatedAt { get; init; }

        /// <inheritdoc/>
        public virtual string DeletedBy { get; init; }

        /// <inheritdoc/>
        public virtual DateTimeOffset? DeletedAt { get; init; }
    }
}