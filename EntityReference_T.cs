// SPDX-FileCopyrightText: Copyright (c) 2022-2024 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;

namespace Envivo.Fresnel.ModelTypes
{
    /// <inheritdoc cref="IEntityReference{TEntity}" />
    public record EntityReference<TEntity> : EntityReference, IEntityReference<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Returns an EntityReference for the given Entity, using ToString() as the Description
        /// </summary>
        /// <param name="entity"></param>
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