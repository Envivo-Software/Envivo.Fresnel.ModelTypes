// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Structural;
using Envivo.Fresnel.ModelTypes.Structural.Classes;
using System;

namespace Envivo.Fresnel.ModelTypes.Temporal.Classes
{
    /// <inheritdoc cref="IDomainEvent" />
    public abstract class BaseDomainEvent : BaseDomainObject, IDomainEvent
    {
        /// <inheritdoc/>
        public virtual DateTimeOffset OccurredAt { get; set; }

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