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
        public static AggregateReference From(IAggregateRoot aggregateRoot) => new(aggregateRoot);

        /// <summary>
        /// Constructor for Serialization/ORM
        /// </summary>
        public AggregateReference() { }

        /// <summary>
        /// Constructor for Serialization/ORM
        /// </summary>
        public AggregateReference(IAggregateRoot aggregateRoot)
        {
            Id = Guid.NewGuid();
            TypeName = aggregateRoot.GetType().FullName;
            AggregateId = aggregateRoot.Id;
            Description = $"{aggregateRoot.GetType().Name}: {aggregateRoot}";
        }

        [Key]
        public Guid Id { get; init; }

        /// <inheritdoc/>
        public string TypeName { get; init; }

        /// <inheritdoc/>
        public Guid AggregateId { get; init; }

        /// <inheritdoc/>
        public string Description { get; init; }

        public override string ToString()
        {
            return Description;
        }
    }
}