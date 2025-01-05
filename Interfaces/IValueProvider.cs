// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    public interface IValueProvider : IDomainDependency { }

    /// <summary>
    /// Used to provide default values of the given type
    /// </summary>
    public interface IValueProvider<TContext, TResult> : IValueProvider
    {
        /// <summary>
        /// Returns a value using the given object as context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public TResult GetValue(TContext context);
    }
}