// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
namespace Envivo.Fresnel.ModelTypes.Persistence.Classes
{
    /// <summary>
    /// The values for filtering and paging against a set of data
    /// </summary>
    /// <remarks>
    /// This is designed to be serialisable, hence the primitive types
    /// </remarks>
    public record QueryFilterDTO
    {
        public QueryFilterDTO() { }

        /// <summary>
        /// The context of this query
        /// </summary>
        public QueryFilterContextDTO FilterContext { get; init; } = new();

        /// <summary>
        /// The filter clause using placeholders for arguments (e.g. WHERE Field1=@0 AND Field2=@1)
        /// </summary>
        public string Filter { get; init; }

        /// <summary>
        /// The values being filtered for. These map to the placeholders in the Filter.
        /// </summary>
        public object[] FilterArgs { get; init; }

        /// <summary>
        /// The sort clause (e.g. Field1 ASC, Field2 DESC}
        /// </summary>
        public string Sort { get; init; }

        /// <summary>
        /// The starting page number (default = 1)
        /// </summary>
        public int? PageNo { get; init; } = 1;

        /// <summary>
        /// The number of items per page
        /// </summary>
        public int? PageSize { get; init; } = 20;
    }
}
