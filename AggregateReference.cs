// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public record AggregateReference<TAggregateRoot> : IAggregateReference<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// Returns an AggregateReference for the given Aggregate root, using ToString() as the Description
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <returns></returns>
        public static IAggregateReference<TAggregateRoot> From(IAggregateRoot aggregateRoot)
        {
            return new AggregateReference<TAggregateRoot>
            {
                Id = Guid.NewGuid(),
                AggregateId = aggregateRoot.Id,
                TypeName = aggregateRoot.GetType().FullName,
                Description = aggregateRoot.ToString()
            };
        }

        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Guid AggregateId { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="aggregateRepository"></param>
        /// <returns></returns>
        public Task<TAggregateRoot> ToAggregateAsync(IRepository<TAggregateRoot> aggregateRepository)
        {
            return aggregateRepository.LoadAsync(this.AggregateId);
        }
    }
}