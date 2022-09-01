// SPDX-FileCopyrightText: Copyright (c) 2022 Envivo Software
// SPDX-License-Identifier: Apache-2.0
namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// A object within a Domain that is described by it's characteristics, not identity.
    /// Recommended to be immutable, but technical constraints (e.g. performance or memory constraints) may dictate otherwise
    /// </summary>
    public interface IValueObject : IDomainObject
    {
    }
}