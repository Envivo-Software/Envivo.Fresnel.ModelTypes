// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
namespace Envivo.Fresnel.ModelTypes.Services
{
    /// <summary>
    /// A set of stateless async operations used to orchestrate with external resources (e.g. Web Services, databases).
    /// Application Services should not contain domain logic. They should provide the informtion needed by the core domain.
    /// </summary>
    public interface IApplicationService : IDomainDependency
    {
    }

    /// <summary>
    /// A set of stateless async operations for a specific Domain Object.
    /// </summary>
    public interface IApplicationService<T> : IApplicationService
        where T : class
    {
    }
}