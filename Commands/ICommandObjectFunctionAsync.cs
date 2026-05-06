// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System.Threading;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Commands
{
    /// <inheritdoc/>
    public interface ICommandFunctionAsync<TResult> : ICommandObjectBase
    where TResult : class
    {
        /// <summary>
        /// Executes the command asynchronously and returns a result 
        /// </summary>
        /// <returns></returns>
        Task<TResult> ExecuteAsync(CancellationToken cancellationToken);
    }

    /// <inheritdoc/>
    public interface ICommandFunctionAsync<TContext, TResult> : ICommandObjectBase
    where TContext : class
    where TResult : class
    {
        /// <summary>
        /// Executes the command asynchronously using the given parameter, and returns a result 
        /// </summary>
        /// <returns></returns>
        Task<TResult> ExecuteAsync(TContext context, CancellationToken cancellationToken);
    }
}