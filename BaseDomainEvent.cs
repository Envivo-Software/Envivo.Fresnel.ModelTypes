// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;

namespace Envivo.Fresnel.ModelTypes
{
    /// <summary>
    /// Captures the state of an Entity (or Entities) at a point in time.
    /// </summary>
    public abstract partial class BaseDomainEvent : BaseDomainObject, IDomainEvent
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual DateTime OccurredAt { get; set; }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;

            IEntity entity = obj as IEntity;
            if (entity == null)
                return false;

            return this.Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}