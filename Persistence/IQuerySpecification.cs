// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Persistence
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

    /// <inheritdoc/>
    public interface IQuerySpecification<TRequestor, TResult> : IQuerySpecification<TResult>
        where TRequestor : class
    {
        /// <summary>
        /// Returns a set of results
        /// </summary>
        Task<IEnumerable<TResult>> GetResultsAsync(TRequestor requestor);
    }
}