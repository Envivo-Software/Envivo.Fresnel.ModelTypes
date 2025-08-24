// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Represents the 'workspace' where entities and objects are found
    /// </summary>
    public interface IModelSpace : IDomainDependency
    {
        /// <summary>
        /// Returns objects from the active identity map.
        /// If not found, returns the item from a repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindAsync<T>(Guid id) where T : class;

        /// <summary>
        /// <inheritdoc cref="FindAsync{T}(Guid)"/>
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<object> FindAsync(string typeName, Guid id);
    }
}
