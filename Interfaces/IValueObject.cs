// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// A object within a Domain that is described by it's characteristics, not identity.
    /// Recommended to be immutable, but technical constraints (e.g. performance or memory constraints) may dictate otherwise
    /// </summary>
    public interface IValueObject : IDomainObject
    {
        /// <summary>
        /// The unique identifier for this ValueObject
        /// </summary>
        Guid Id { get; set; }
    }
}