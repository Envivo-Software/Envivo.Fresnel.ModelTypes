// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System.Threading;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Commands
{
    /// <inheritdoc/>
    public interface ICommandObjectAsync : ICommandObjectBase
    {
        /// <summary>
        /// Executes the command asynchronously
        /// </summary>
        /// <returns></returns>
        Task ExecuteAsync(CancellationToken cancellationToken);
    }

    /// <inheritdoc/>
    public interface ICommandObjectAsync<TContext> : ICommandObjectBase
        where TContext : class
    {
        /// <summary>
        /// Executes the command asynchronously using the given parameter
        /// </summary>
        /// <returns></returns>
        Task ExecuteAsync(TContext context, CancellationToken cancellationToken);
    }
}
