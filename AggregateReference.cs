// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

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
        public static AggregateReference<TAggregateRoot> From(IAggregateRoot aggregateRoot)
        {
            var type = aggregateRoot.GetType();
            return new AggregateReference<TAggregateRoot>
            {
                Id = Guid.NewGuid(),
                AggregateId = aggregateRoot.Id,
                TypeName = type.FullName,
                Description = $"{type.Name}: {aggregateRoot}"
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

        public override string ToString()
        {
            return Description;
        }
    }
}