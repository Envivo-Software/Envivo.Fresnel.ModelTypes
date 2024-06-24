// SPDX-FileCopyrightText: Copyright (c) 2022-2024 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.ComponentModel.DataAnnotations;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// A memento/token that provides a reference to an Entity
    /// </summary>
    public interface IEntityReference : IDomainObject
    {
        /// <summary>
        /// The unique identifier for this Entity Reference
        /// </summary>
        [Key]
        public Guid Id { get; }

        /// <summary>
        /// The referenced Entity's Type
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        /// The referenced Entity's ID
        /// </summary>
        public Guid EntityId { get; }

        /// <summary>
        /// A user-friendly description of the referenced Entity
        /// </summary>
        public string Description { get; }
    }

    /// <summary>
    /// A memento/token that provides a reference to an Entity in the _same_ Aggregate.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityReference<TEntity> : IEntityReference
        where TEntity : IEntity
    {

    }
}
