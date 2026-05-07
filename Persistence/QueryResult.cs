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

        /// <inheritdoc/>
        public int PageNumber { get; }

        /// <inheritdoc/>
        public int PageSize { get; }

        /// <inheritdoc/>
        public int TotalItems { get; }

        /// <inheritdoc/>
        public int TotalPages { get; }
    }

    public record QueryResult<TObject> : QueryResult
    {
        public QueryResult(IEnumerable<TObject> items, int pageNumber = 1, int pageSize = 20, int totalItems = -1, int totalPages = -1)
            : base(pageNumber, pageSize, totalItems, totalPages)
        {
            Items = items;
        }

        /// <inheritdoc/>
        public IEnumerable<TObject> Items { get; init; }
    }
}
