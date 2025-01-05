// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.ComponentModel;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Encapsulates a command that requires more interaction or behaviour than a standard Method
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ICommandObjectBase
    {
        Guid Id { get; }

        /// <summary>
        /// Returns TRUE if the command is ready to be executed
        /// </summary>
        bool IsReadyToExecute { get; }
    }

    /// <inheritdoc/>
    public interface ICommandObject : ICommandObjectBase
    {
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns></returns>
        void Execute();
    }

    /// <inheritdoc/>
    public interface ICommandObject<TContext> : ICommandObjectBase
        where TContext : class
    {
        /// <summary>
        /// Executes the command using the given parameter
        /// </summary>
        /// <returns></returns>
        void Execute(TContext context);
    }

    /// <inheritdoc/>
    public interface ICommandObject<TContext, TResult> : ICommandObjectBase
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