// SPDX-FileCopyrightText: Copyright (c) 2022-2024 Envivo Software
// SPDX-License-Identifier: Apache-2.0
namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// A set of stateless operations, whose behaviours cannot be contained within any Domain Object.
    /// Domain Services should not be confused with Application/Web Services, or Infrastructure services.
    /// </summary>
    public interface IDomainService : IDomainDependency
    {
    }
}