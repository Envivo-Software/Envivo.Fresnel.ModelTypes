// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// The values for filtering and paging against a set of data
    /// </summary>
    public interface IQueryFilter
    {
        /// <summary>
        /// The type of the Class being searched for
        /// </summary>
        public string ClassTypeName { get; set; }

        /// <summary>
        /// The filter clause using placeholders for arguments (e.g. WHERE Field1=@0 AND Field2=@1)
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// The values being filtered for. These map to the placeholders in the Filter.
        /// </summary>
        public object[] FilterArgs { get; set; }

        /// <summary>
        /// The sort clause (e.g. Field1 ASC, Field2 DESC}
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// The starting page number (default = 1)
        /// </summary>
        public int? PageNo { get; set; }

        /// <summary>
        /// The number of items per page
        /// </summary>
        public int? PageSize { get; set; }
    }
}