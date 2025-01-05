// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// A memento/token that provides a reference to an Aggregate Root in a _different_ Bounded Context.
    /// </summary>
    public interface IAggregateReference : IDomainObject
    {
        /// <summary>
        /// The unique identifier for this Aggregate Reference
        /// </summary>
        [Key]
        public Guid Id { get; }

        /// <summary>
        /// The referenced Aggregate's Type
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        /// The referenced Aggregate's ID
        /// </summary>
        public Guid AggregateId { get; }

        /// <summary>
        /// A user-friendly description that remains accurate, regardless of any changes to the referenced Aggregate itself
        /// </summary>
        public string Description { get; }
    }

    /// <summary>
    /// A memento/token that provides a reference to an Aggregate Root in the _same_ Bounded Context.
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public interface IAggregateReference<TAggregateRoot> : IAggregateReference
    where TAggregateRoot : class, IAggregateRoot
    {

        /// <summary>
        /// Returns the Aggregate that matches this reference
        /// </summary>
        /// <param name="aggregateRepository">The repository containing the Aggregate</param>
        /// <returns></returns>
        Task<TAggregateRoot> ToAggregateAsync(IRepository<TAggregateRoot> aggregateRepository)
        {
            return aggregateRepository.LoadAsync(AggregateId);
        }
    }
}
