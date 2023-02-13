// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IQuerySpecification : IDomainDependency
    { }

    /// <summary>
    /// Used to encapsulate queries that get executed against a data store
    /// </summary>
    public interface IQuerySpecification<TResult> : IQuerySpecification
    {
        /// <summary>
        /// Returns a set of results
        /// </summary>
        Task<IEnumerable<TResult>> GetResultsAsync();
    }

    public interface IQuerySpecification<TRequestor, TResult> : IQuerySpecification<TResult>
        where TRequestor : class
    {
        /// <summary>
        /// Returns a set of results
        /// </summary>
        Task<IEnumerable<TResult>> GetResultsAsync(TRequestor requestor);
    }
}