// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
namespace Envivo.Fresnel.ModelTypes.Commands
{
    /// <inheritdoc/>
    public interface ICommandFunction<out TResult> : ICommandObjectBase
    where TResult : class
    {
        /// <summary>
        /// Executes the command and returns a result 
        /// </summary>
        /// <returns></returns>
        TResult Execute();
    }

    /// <inheritdoc/>
    public interface ICommandFunction<TContext, TResult> : ICommandObjectBase
    where TContext : class
    where TResult : class
    {
        /// <summary>
        /// Executes the command using the given parameter, and returns a result 
        /// </summary>
        /// <returns></returns>
        TResult Execute(TContext context);
    }
}
