﻿// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Any object that may be persisted to a database
    /// </summary>
    public interface IPersistable : IDomainObject
    {
        /// <summary>
        /// The unique identifier for this Entity. Typically marked with [KeyAttribute].
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Used for concurrency checks. Typically marked with [ConcurrencyCheckAttribute].
        /// </summary>
        long Version { get; set; }
    }
}