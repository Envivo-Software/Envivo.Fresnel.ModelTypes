// SPDX-FileCopyrightText: Copyright (c) 2022 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Any object within a Domain that has unique identity.
    /// It may be necessary to override Equals() so that comparisons are made by Id.
    /// </summary>
    public interface IEntity : IDomainObject
    {
        /// <summary>
        /// The unique identifier for this Entity
        /// </summary>
        Guid Id { get; set; }
    }
}