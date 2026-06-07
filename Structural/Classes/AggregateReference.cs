// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.ComponentModel.DataAnnotations;

namespace Envivo.Fresnel.ModelTypes.Structural.Classes
{
    /// <inheritdoc cref="IAggregateReference" />
    public record AggregateReference : IAggregateReference
    {
        /// <summary>
        /// Returns an AggregateReference for the given Aggregate root, using ToString() as the Description
        /// </summary>
        /// <param name="aggregateRoot">The Aggregate being referenced</param>
        /// <returns></returns>
        public static AggregateReference From(IAggregateRoot aggregateRoot)
        {
            var type = aggregateRoot.GetType();
            return new AggregateReference
            {
                Id = Guid.NewGuid(),
                AggregateId = aggregateRoot.Id,
                TypeName = type.FullName,
                Description = $"{type.Name}: {aggregateRoot}"
            };
        }

        [Key]
        public Guid Id { get; set; }

        /// <inheritdoc/>
        public string TypeName { get; set; }

        /// <inheritdoc/>
        public Guid AggregateId { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}