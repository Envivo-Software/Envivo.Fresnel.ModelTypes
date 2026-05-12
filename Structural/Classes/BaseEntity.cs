// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
namespace Envivo.Fresnel.ModelTypes.Structural.Classes
{
    /// <inheritdoc cref="IEntity" />
    public abstract class BaseEntity : BaseDomainObject, IEntity
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