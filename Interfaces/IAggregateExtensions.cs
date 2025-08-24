using System;
using System.Threading.Tasks;
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace Envivo.Fresnel.ModelTypes
{
    public static class IAggregateExtensions
    {
        /// <summary>
        /// Resolves the actual Aggregate from the active model space
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reference"></param>
        /// <param name="modelSpace"></param>
        /// <returns></returns>
        public static Task<T> ResolveAsync<T>(this IAggregateReference reference, IModelSpace modelSpace)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(modelSpace);
            return modelSpace.FindAsync<T>(reference.AggregateId);
        }

        /// <summary>
        /// <inheritdoc cref="ResolveAsync{T}(IAggregateReference, IModelSpace)"/>
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="reference"></param>
        /// <param name="modelSpace"></param>
        /// <returns></returns>
        public static Task<TAggregateRoot> ResolveAsync<TAggregateRoot>(this IAggregateReference<TAggregateRoot> reference, IModelSpace modelSpace)
            where TAggregateRoot : class, IAggregateRoot
        {
            ArgumentNullException.ThrowIfNull(modelSpace);
            return modelSpace.FindAsync<TAggregateRoot>(reference.AggregateId);
        }
    }
}
