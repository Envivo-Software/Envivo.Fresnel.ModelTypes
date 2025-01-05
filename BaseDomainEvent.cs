// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;

namespace Envivo.Fresnel.ModelTypes
{
    /// <inheritdoc cref="IDomainEvent" />
    public abstract partial class BaseDomainEvent : BaseDomainObject, IDomainEvent
    {
        /// <inheritdoc/>
        public virtual DateTime OccurredAt { get; set; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj, o => o.Id);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode(o => o.Id);
        }
    }
}