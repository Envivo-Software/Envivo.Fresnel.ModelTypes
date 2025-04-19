// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.Collections.Generic;
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// A Calendar of entries
    /// </summary>
    public interface ICalendar : IEntity
    {
        /// <summary>
        /// An overall title for this calendar
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Returns the entries for the given Range
        /// </summary>
        /// <param name="rangeStart"></param>
        /// <param name="rangeEnd"></param>
        /// <returns></returns>
        public IEnumerable<ICalendarEntry> GetEntries(DateTime rangeStart, DateTime rangeEnd);

        /// <summary>
        /// An entry within a calendar
        /// </summary>
        public interface ICalendarEntry : IValueObject
        {
            /// <summary>
            /// The data &amp; time for this entry
            /// </summary>
            public DateTime EntryDate { get; }

            /// <summary>
            /// The duration of this entry
            /// </summary>
            public TimeSpan Duration { get; }

            /// <summary>
            /// A headline title for this entry
            /// </summary>
            public string Title { get; }

            /// <summary>
            /// A longer description for this entry
            /// </summary>
            public string Description { get; }
        }
    }
}
