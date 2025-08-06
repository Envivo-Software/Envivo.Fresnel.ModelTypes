// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    public interface IJsonObjectSerializer : IDomainDependency
    {
        public string Serialize<T>(T obj) where T : class;

        public object Deserialize(string json, Type type);

        public T Deserialize<T>(string json) where T : class;
    }
}
