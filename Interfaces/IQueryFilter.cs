// SPDX-FileCopyrightText: Copyright (c) 2022-2024 Envivo Software
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// The values for filtering and paging against a set of data
    /// </summary>
    public interface IQueryFilter
    {
        /// The type of the Class being searched for
        /// The context of this query
        /// </summary>
        [Obsolete("Use FilterContext.ClassName instead")]
        public string ClassTypeName { get; set; }

        /// <summary>
        /// The context of this query
        /// </summary>
        public IQueryFilterContext FilterContext { get; }

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

    /// <summary>
    /// The context within which a QueryFilter is executed
    /// </summary>
    public interface IQueryFilterContext
    {
        /// <summary>
        /// The type of the Class being searched for
        /// </summary>
        public string ClassTypeName { get; set; }

        /// <summary>
        /// The parent object (if applicable)
        /// </summary>
        public Guid ObjectId { get; set; }

        /// <summary>
        /// The name of the QuerySpecification to be executed
        /// </summary>
        public string QuerySpecificationTypeName { get; set; }

        /// <summary>
        /// The name of the Property being searched against (if applicable)
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The name of the Parameter being searched against (if applicable)
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// The name of the Method that the ParameterName belongs to (if applicable)
        /// </summary>
        public string MethodName { get; set; }
    }
}
