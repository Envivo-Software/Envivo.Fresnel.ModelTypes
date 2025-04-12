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
            public DateTime EntryDate { get; }

            public TimeSpan Duration { get; }

            public string Title { get; }

            public string Description { get; }
        }
    }
}
