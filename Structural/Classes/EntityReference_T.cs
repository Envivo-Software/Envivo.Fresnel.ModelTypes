// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Structural.Classes
{
    /// <inheritdoc cref="IEntityReference{TEntity}" />
    public record EntityReference<TEntity> : EntityReference, IEntityReference<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Returns an EntityReference for the given Entity, using ToString() as the Description
        /// </summary>
        /// <param name="entity">The entity being referenced</param>
        /// <returns></returns>
        public static EntityReference<TEntity> From(TEntity entity)
        {
            var type = entity.GetType();
            return new EntityReference<TEntity>
            {
                Id = Guid.NewGuid(),
                EntityId = entity.Id,
                TypeName = type.FullName,
                Description = $"{type.Name}: {entity}"
            };
        }

        public override string ToString()
        {
            return Description;
        }
    }
}