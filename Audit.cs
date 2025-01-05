// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Envivo.Fresnel.ModelTypes
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
        public virtual DateTime? CreatedAt { get; set; }

        /// <inheritdoc/>
        public virtual string UpdatedBy { get; set; }

        /// <inheritdoc/>
        public virtual DateTime? UpdatedAt { get; set; }

        /// <inheritdoc/>
        public virtual string DeletedBy { get; set; }

        /// <inheritdoc/>
        public virtual DateTime? DeletedAt { get; set; }
    }
}