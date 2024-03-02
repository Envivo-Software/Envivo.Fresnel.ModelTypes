// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// A set of stateless operations that are exposed to external consumers (e.g. Web Services).
    /// Application Services should not be confused with Domain Services or Infrastructure services.
    /// </summary>
    public interface IApplicationService : IDomainDependency
    {
    }
}