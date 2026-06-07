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
        /// Returns an EntityReference for the given Entity, using ToString() as the Description.
        /// Use this instead of the constructor.
        /// </summary>
        /// <param name="entity">The entity being referenced</param>
        /// <returns></returns>
        public static EntityReference From(IEntity entity) => new(entity);

        /// <summary>
        /// Constructor for Serialization/ORM
        /// </summary>
        public EntityReference() { }

        /// <summary>
        /// Constructor for Serialization/ORM
        /// </summary>
        public EntityReference(IEntity entity)
        {
            Id = Guid.NewGuid();
            TypeName = entity.GetType().FullName;
            EntityId = entity.Id;
            Description = $"{entity.GetType().Name}: {entity}";
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