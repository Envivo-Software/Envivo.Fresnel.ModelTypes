// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Services;

namespace Envivo.Fresnel.ModelTypes.Infrastructure
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