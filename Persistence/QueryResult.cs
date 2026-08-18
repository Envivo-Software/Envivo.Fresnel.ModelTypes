// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System.Collections.Generic;

namespace Envivo.Fresnel.ModelTypes.Persistence
{
    public record QueryResult
    {
        public QueryResult(int pageNumber = 1, int pageSize = 20, int totalItems = -1, int totalPages = -1)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

        /// <summary>
        /// The requested page number
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// The requested page size
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// The total possible number of items that match
        /// </summary>
        public int TotalItems { get; }

        /// <summary>
        /// The total possible pages of results
        /// </summary>
        public int TotalPages { get; }
    }

    public record QueryResult<TObject> : QueryResult
    {
        public QueryResult(IEnumerable<TObject> items, int pageNumber = 1, int pageSize = 20, int totalItems = -1, int totalPages = -1)
            : base(pageNumber, pageSize, totalItems, totalPages)
        {
            Items = items;
        }

        /// <summary>
        /// The items returned
        /// </summary>
        public IEnumerable<TObject> Items { get; init; }
    }
}
