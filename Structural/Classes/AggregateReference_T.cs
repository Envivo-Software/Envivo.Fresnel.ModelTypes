// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
namespace Envivo.Fresnel.ModelTypes.Structural.Classes
{
    /// <inheritdoc cref="IAggregateReference{TAggregateRoot}" />
    public record AggregateReference<TAggregateRoot> : AggregateReference, IAggregateReference<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// Returns an AggregateReference for the given Aggregate root, using ToString() as the Description
        /// </summary>
        /// <param name="aggregateRoot">The Aggregate being referenced</param>
        /// <returns></returns>
        public static AggregateReference<TAggregateRoot> From(TAggregateRoot aggregateRoot) => new(aggregateRoot);

        /// <summary>
        /// Constructor for Serialization/ORM
        /// </summary>
        public AggregateReference() { }

        public AggregateReference(TAggregateRoot aggregateRoot)
            : base(aggregateRoot)
        { }

        public override string ToString()
        {
            return Description;
        }
    }
}