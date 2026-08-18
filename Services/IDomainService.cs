// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
namespace Envivo.Fresnel.ModelTypes.Services
{
    /// <summary>
    /// A set of stateless async operations, whose behaviours cannot be contained within any Domain Object.
    /// </summary>
    public interface IDomainService : IDomainDependency
    {
    }

    /// <summary>
    /// A set of stateless async operations for a specific Domain Object.
    /// </summary>
    public interface IDomainService<T> : IDomainService
    {
    }
}