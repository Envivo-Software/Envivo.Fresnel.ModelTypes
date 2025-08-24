using System;
using System.Threading.Tasks;
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace Envivo.Fresnel.ModelTypes
{
    public static class IEntityExtensions
    {
        /// <summary>
        /// Resolves the actual Entity from the active model space
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reference"></param>
        /// <param name="modelSpace"></param>
        /// <returns></returns>
        public static Task<T> ResolveAsync<T>(this IEntityReference reference, IModelSpace modelSpace)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(modelSpace);
            return modelSpace.FindAsync<T>(reference.EntityId);
        }

        /// <summary>
        /// <inheritdoc cref="ResolveAsync{T}(IEntityReference, IModelSpace)"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="reference"></param>
        /// <param name="modelSpace"></param>
        /// <returns></returns>
        public static Task<TEntity> ResolveAsync<TEntity>(this IEntityReference<TEntity> reference, IModelSpace modelSpace)
            where TEntity : class, IEntity
        {
            ArgumentNullException.ThrowIfNull(modelSpace);
            return modelSpace.FindAsync<TEntity>(reference.EntityId);
        }
    }
}
