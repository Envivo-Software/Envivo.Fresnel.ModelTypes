// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Used to configure an ICommandObject with an initial set of values
    /// </summary>
    public interface ICommandObjectSetup<TSetupContext>
        where TSetupContext : class
    {
        /// <summary>
        /// Configures the command from the given object
        /// </summary>
        /// <returns></returns>
        Task SetupAsync(TSetupContext context);
    }
}
