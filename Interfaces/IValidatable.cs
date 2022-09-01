// SPDX-FileCopyrightText: Copyright (c) 2022 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System.ComponentModel;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Used to validate Domain Objects
    /// </summary>
    public interface IValidatable : IDataErrorInfo
    {
        /// <summary>
        /// Returns TRUE if the Domain Object is in a valid state. The Error property provides details of actual problems.
        /// </summary>
        bool IsValid();
    }
}