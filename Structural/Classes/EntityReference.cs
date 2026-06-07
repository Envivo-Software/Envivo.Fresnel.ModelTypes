// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.ComponentModel.DataAnnotations;

namespace Envivo.Fresnel.ModelTypes.Structural.Classes
{
    /// <inheritdoc cref="IEntityReference" />
    public record EntityReference : IEntityReference
    {
        /// <summary>
        /// Returns an EntityReference for the given Entity, using ToString() as the Description
        /// </summary>
        /// <param name="entity">The entity being referenced</param>
        /// <returns></returns>
        public static EntityReference From(IEntity entity)
        {
            var type = entity.GetType();
            return new EntityReference
            {
                Id = Guid.NewGuid(),
                EntityId = entity.Id,
                TypeName = type.FullName,
                Description = $"{type.Name}: {entity}"
            };
        }

        [Key]
        public Guid Id { get; init; }

        /// <inheritdoc/>
        public string TypeName { get; init; }

        /// <inheritdoc/>
        public Guid EntityId { get; init; }

        /// <inheritdoc/>
        public string Description { get; init; }

        public override string ToString()
        {
            return Description;
        }
    }
}