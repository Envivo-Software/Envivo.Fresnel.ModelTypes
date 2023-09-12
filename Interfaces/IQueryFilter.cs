// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// The values for filtering and paging against a set of data
    /// </summary>
    public interface IQueryFilter
    {
        public string ClassTypeName { get; set; }

        public string Filter { get; set; }

        public object[] FilterArgs { get; set; }

        public string Sort { get; set; }

        public int? PageNo { get; set; }

        public int? PageSize { get; set; }
    }
}