// SPDX-FileCopyrightText: Copyright (c) 2022 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace Envivo.Fresnel.ModelTypes
{
    /// <summary>
    /// Any object within a Domain that has unique identity.
    /// </summary>
    public abstract partial class BaseEntity : BaseDomainObject, IEntity
    {
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;

            var that = obj as BaseDomainObject;
            if (that == null)
                return false;

            return this.Id.Equals(that.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}