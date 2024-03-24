// SPDX-FileCopyrightText: Copyright (c) 2022-2024 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Used to apply a pessimistic lock to an Aggregate Root
    /// </summary>
    public interface IAggregateLock
    {
        /// <summary>
        /// Reference to the Aggregate Root being locked
        /// </summary>
        IAggregateReference<IAggregateRoot> AggregateReference { get; set; }

        /// <summary>
        /// The user that locked the Aggregate
        /// </summary>
        string LockedBy { get; set; }

        /// <summary>
        /// The time when the lock should be released. Using an 'end time' should prevent rogue locks blocking usage indefinitely.
        /// </summary>
        DateTimeOffset LockedUntil { get; set; }
    }
}