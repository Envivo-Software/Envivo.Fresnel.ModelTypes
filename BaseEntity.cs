// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace Envivo.Fresnel.ModelTypes
{
    /// <inheritdoc cref="IEntity" />
    public abstract partial class BaseEntity : BaseDomainObject, IEntity
    {
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