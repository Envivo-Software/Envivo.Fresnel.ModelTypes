// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Structural.Classes
{
    /// <inheritdoc cref="IAggregateReference{TAggregateRoot}" />
    public record AggregateReference<TAggregateRoot> : AggregateReference, IAggregateReference<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// Returns an AggregateReference for the given Aggregate root, using ToString() as the Description
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <returns></returns>
        public static new AggregateReference<TAggregateRoot> From(IAggregateRoot aggregateRoot)
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

        public override string ToString()
        {
            return Description;
        }
    }
}