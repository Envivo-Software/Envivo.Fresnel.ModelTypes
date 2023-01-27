// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System.ComponentModel;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IConsistencyCheck : IDomainDependency
    { }

    /// <summary>
    /// Applies checks against a Domain object, to ensure it is fit for persisting
    /// </summary>
    public interface IConsistencyCheck<T> : IConsistencyCheck
    {
        /// <summary>
        /// Determines if the given Domain Object if fit for persisting
        /// </summary>
        /// <param name="obj"></param>
        Assertion Check(T obj);
    }
}