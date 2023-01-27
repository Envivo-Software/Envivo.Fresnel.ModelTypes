// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;

namespace Envivo.Fresnel.ModelTypes
{
    /// <summary>
    /// A object within a Domain that is described by it's characteristics, not identity.
    /// </summary>
    public abstract partial class BaseValueObject : BaseDomainObject, IValueObject
    {
        public override bool Equals(object obj)
        {
            throw new MethodAccessException("Value Objects must be compared using property values. Please override the Equals() method and implement the comparison");
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}