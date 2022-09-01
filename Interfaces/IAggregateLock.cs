// SPDX-FileCopyrightText: Copyright (c) 2022 Envivo Software
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
        /// The Aggregate Root being locked
        /// </summary>
        IAggregateRoot AggregateRoot { get; set; }

        /// <summary>
        /// The user that locked the Aggregate
        /// </summary>
        string LockedBy { get; set; }

        /// <summary>
        /// The time when the lock should be released. Using an 'end time' should prevent rogue locks blocking usage indefinitely.
        /// </summary>
        DateTime LockedUntil { get; set; }
    }
}