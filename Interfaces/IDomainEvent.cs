﻿// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Captures the state of an Entity (or Entities) at a point in time.
    /// Each Domain Event is unique and should NOT be treated as a Value Object.
    /// However, Domain Events should be immutable to avoid the risk of corruption.
    /// </summary>
    public interface IDomainEvent : IDomainObject
    {
        /// <summary>
        /// The unique identifier for this Domain Event
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// The date/time when the Domain Event occurred
        /// </summary>
        DateTime OccurredAt { get; }
    }
}