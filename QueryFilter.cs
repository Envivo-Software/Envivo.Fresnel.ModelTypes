// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace Envivo.Fresnel.ModelTypes
{
    public class QueryFilter : IQueryFilter
    {
        public string ClassTypeName { get; set; }

        public string Filter { get; set; }
        public object[] FilterArgs { get; set; }
        public string Sort { get; set; }
        public int? PageNo { get; set; } = 1;
        public int? PageSize { get; set; } = 20;
    }
}