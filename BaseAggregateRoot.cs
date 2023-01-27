// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace Envivo.Fresnel.ModelTypes
{
    /// <summary>
    /// Groups closely related objects, and provides a transactional boundary around them.
    /// </summary>
    public abstract partial class BaseAggregateRoot : BaseDomainObject, IAggregateRoot
    {
    }
}