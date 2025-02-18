﻿// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;

namespace Envivo.Fresnel.ModelTypes
{
    /// <inheritdoc cref="IAggregateLock" />
    public class AggregateLock : IAggregateLock
    {
        /// <inheritdoc/>
        public IAggregateReference<IAggregateRoot> AggregateReference { get; set; }

        /// <inheritdoc/>
        public string LockedBy { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset LockedUntil { get; set; }
    }
}