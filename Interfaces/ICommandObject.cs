// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Encapsulates a command that requires more interaction or behaviour than a standard Method
    /// </summary>
    public interface ICommandObject
    {
        Guid Id { get; }

        /// <summary>
        /// Returns TRUE if the command is ready to be executed
        /// </summary>
        bool IsReadyToExecute { get; }

        /// <summary>
        /// Executes the command, and returns an optional result 
        /// </summary>
        /// <returns></returns>
        object Execute();
    }
}