// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Envivo.Fresnel.ModelTypes.Rules.Classes
{
    /// <summary>
    /// Represents the result of a rule assertion, tracking whether it passed, failed, or passed with warnings.
    /// </summary>
    public class Assertion
    {
        private static Assertion _Pass = new Assertion() { HasPassed = true };

        /// <summary>
        /// Gets a singleton <see cref="Assertion"/> that indicates a successful assertion with no result value.
        /// </summary>
        public static Assertion Pass
        {
            get { return _Pass; }
        }

        /// <summary>
        /// Creates an <see cref="Assertion"/> that has passed but includes a <see cref="WarningException"/>.
        /// </summary>
        /// <param name="warning">The warning that accompanies the passing assertion.</param>
        /// <returns>An <see cref="Assertion"/> indicating success with a warning.</returns>
        public static Assertion PassWithWarning(WarningException warning)
        {
            return new Assertion()
            {
                HasPassed = true,
                Warning = warning
            };
        }

        /// <summary>
        /// Creates an <see cref="Assertion"/> that has failed with a single exception.
        /// </summary>
        /// <param name="failure">The <see cref="Exception"/> that caused the assertion to fail.</param>
        /// <returns>An <see cref="Assertion"/> indicating failure.</returns>
        public static Assertion Fail(Exception failure)
        {
            if (failure == null)
                throw new ArgumentNullException(nameof(failure));

            return new Assertion()
            {
                HasFailed = true,
                FailureException = failure
            };
        }
        /// <summary>
        /// Creates an <see cref="Assertion"/> that has failed with multiple exceptions, aggregated into an <see cref="AggregateException"/>.
        /// </summary>
        /// <param name="failures">An <see cref="IEnumerable{T}"/> of <see cref="Exception"/> objects that caused the assertion to fail.</param>
        /// <returns>An <see cref="Assertion"/> indicating failure.</returns>
        public static Assertion Fail(IEnumerable<Exception> failures)
        {
            if (failures == null)
                throw new ArgumentNullException(nameof(failures));

            return Fail(new AggregateException(failures));
        }

        /// <summary>
        /// Creates an <see cref="Assertion"/> that has failed with an exception but also includes a <see cref="WarningException"/>.
        /// </summary>
        /// <param name="failure">The <see cref="Exception"/> that caused the assertion to fail.</param>
        /// <param name="warning">The <see cref="WarningException"/> that accompanies the failed assertion.</param>
        /// <returns>An <see cref="Assertion"/> indicating failure with a warning.</returns>
        public static Assertion FailWithWarning(Exception failure, WarningException warning)
        {
            if (failure == null)
                throw new ArgumentNullException(nameof(failure));

            return new Assertion()
            {
                HasFailed = true,
                FailureException = failure,
                Warning = warning
            };
        }

        /// <summary>
        /// Retrieves a formatted message describing any failure that occurred during the assertion.
        /// If multiple exceptions were aggregated, their messages are joined with newline separators.
        /// </summary>
        /// <returns>A string containing the failure message, or null if the assertion passed.</returns>
        public string GetFailureMessage()
        {
            var aggregateException = this.FailureException as AggregateException;

            if (aggregateException != null)
            {
                var exceptions = aggregateException.Flatten().InnerExceptions;
                var messages = exceptions.Select(e => e.Message).ToArray();
                return string.Join(Environment.NewLine, messages);
            }
            else
            {
                return this.FailureException?.Message;
            }
        }

        public bool HasPassed { get; protected set; }

        public bool HasFailed { get; protected set; }

        public bool HasWarning { get { return this.Warning != null; } }

        public Exception Warning { get; protected set; }

        public Exception FailureException { get; protected set; }
    }
}