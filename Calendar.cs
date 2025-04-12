using System;
using System.Collections.Generic;
using System.Linq;
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace Envivo.Fresnel.ModelTypes
{
    /// <inheritdoc/>
    public class Calendar : ICalendar
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }

        /// <inheritdoc/>
        public string Title { get; set; }

        public ICollection<CalendarEntry> Entries { get; set; } = new List<CalendarEntry>();

        /// <inheritdoc/>
        public IEnumerable<ICalendar.ICalendarEntry> GetEntries(DateTime rangeStart, DateTime rangeEnd)
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
            public Guid Id { get; set; }

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
