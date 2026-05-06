// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Rules.Classes;
using Envivo.Fresnel.ModelTypes.Services;
using System.ComponentModel;

namespace Envivo.Fresnel.ModelTypes.Rules
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ISpecification : IDomainDependency
    { }

    /// <summary>
    /// Encapsulates business rules (aka invariants) to be made against a Domain Object
    /// </summary>
    public interface ISpecification<T> : ISpecification
        where T : class
    {
        /// <summary>
        /// Determines if this specification is met by the given Domain Object
        /// </summary>
        /// <param name="obj"></param>
        Assertion IsSatisfiedBy(T obj);
    }
}