﻿using System.ComponentModel;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFactory : IDomainDependency
    { }

    /// <summary>
    /// A set of stateless methods for creating complex Domain Objects of the given type.
    /// Consider using Factories when object constructors start getting complicated.
    /// </summary>
    public interface IFactory<T> : IFactory
        where T : class
    {
        /// <summary>
        /// Returns a new instance of the generic type
        /// </summary>
        /// <returns></returns>
        T Create();
    }
}