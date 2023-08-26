// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;

namespace Envivo.Fresnel.ModelTypes
{
    /// <summary>
    /// Used to apply a pessimistic lock to an Aggregate Root
    /// </summary>
    public class AggregateLock : IAggregateLock
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IAggregateReference<IAggregateRoot> AggregateReference { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string LockedBy { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTimeOffset LockedUntil { get; set; }
    }
}