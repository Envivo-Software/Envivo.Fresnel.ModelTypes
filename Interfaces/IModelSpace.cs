// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
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
        /// Ensures the given objects are tracked by the active identity map
        /// </summary>
        /// <param name="domainObject"></param>
        /// <param name="additionalObjects"></param>
        void AttachObjects(object domainObject, params object[] additionalObjects);

        /// <summary>
        /// Ensures the given object and it's entire graph are tracked by the active identity map
        /// </summary>
        /// <param name="rootObject"></param>
        void AttachObjectGraph(object rootObject);

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
