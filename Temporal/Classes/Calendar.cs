// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.Collections.Generic;
using System.Linq;

namespace Envivo.Fresnel.ModelTypes.Temporal.Classes
{
    /// <inheritdoc/>
    public class Calendar : ICalendar
    {
        /// <inheritdoc/>
        public Guid Id { get; init; }

        /// <inheritdoc/>
        public string Title { get; set; }

        public ICollection<CalendarEntry> Entries { get; set; } = new List<CalendarEntry>();

        /// <inheritdoc/>
        public IEnumerable<ICalendar.ICalendarEntry> GetEntries(DateTimeOffset rangeStart, DateTimeOffset rangeEnd)
        {
            return
                Entries
                .Where(e => e.EntryDate >= rangeStart &&
                            e.EntryDate <= rangeEnd)
                .ToList();
        }

        /// <inheritdoc/>
        public class CalendarEntry : ICalendar.ICalendarEntry
        {
            /// <inheritdoc/>
            public Guid Id { get; init; }

            /// <inheritdoc/>
            public DateTime EntryDate { get; set; }

            /// <inheritdoc/>
            public TimeSpan Duration { get; set; }

            /// <inheritdoc/>
            public string Title { get; set; }

            /// <inheritdoc/>
            public string Description { get; set; }
        }
    }
}
