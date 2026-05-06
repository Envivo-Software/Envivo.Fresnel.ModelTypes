// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0

namespace Envivo.Fresnel.ModelTypes.Structural
{
    /// <summary>
    /// Groups closely related objects. Aggregates Roots are used to:
    /// (1) Provide an access route for Domain Objects within the cluster
    /// (2) Enforce rules and consistency for the entire cluster of objects
    /// (3) Provide a suitable locking point for the cluster in a multi-user environment
    /// </summary>
    public interface IAggregateRoot : IEntity
    {
        
    }
}
