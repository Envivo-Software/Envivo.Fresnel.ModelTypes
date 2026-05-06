// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Services;
using System;

namespace Envivo.Fresnel.ModelTypes.Infrastructure
{
    public interface IJsonObjectSerializer : IDomainDependency
    {
        public string Serialize<T>(T obj) where T : class;

        public object Deserialize(string json, Type type);

        public T Deserialize<T>(string json) where T : class;
    }
}
