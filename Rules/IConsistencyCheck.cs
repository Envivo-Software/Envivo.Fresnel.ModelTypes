// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Rules.Classes;
using Envivo.Fresnel.ModelTypes.Services;
using System.ComponentModel;

namespace Envivo.Fresnel.ModelTypes.Rules
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IConsistencyCheck : IDomainDependency
    { }

    /// <summary>
    /// Applies checks against a Domain object, to ensure it is fit for persisting
    /// </summary>
    public interface IConsistencyCheck<T> : IConsistencyCheck
        where T : class
    {
        /// <summary>
        /// Determines if the given Domain Object is fit for persisting
        /// </summary>
        /// <param name="obj"></param>
        Assertion Check(T obj);
    }
}