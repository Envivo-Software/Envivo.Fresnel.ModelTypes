﻿// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Provides a reference to another Aggregate Root
    /// </summary>
    public interface IAggregateReference<TAggregateRoot> : IValueObject, IPersistable
        where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// The referenced Aggregate's Type
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        /// The referenced Aggregate's ID
        /// </summary>
        public Guid AggregateId { get; }

        /// <summary>
        /// A user-friendly description that remains accurate, regardless of any changes to the Aggregate itself
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Returns the Aggregate that matches this reference
        /// </summary>
        /// <param name="aggregateRepository">The repository containing the Aggregate</param>
        /// <returns></returns>
        Task<TAggregateRoot> ToAggregateAsync(IRepository<TAggregateRoot> aggregateRepository);
    }
}