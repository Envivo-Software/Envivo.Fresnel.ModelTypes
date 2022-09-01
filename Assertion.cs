// SPDX-FileCopyrightText: Copyright (c) 2022 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Envivo.Fresnel.ModelTypes
{
    public class Assertion
    {
        private static Assertion _Pass = new Assertion() { HasPassed = true };

        public static Assertion Pass
        {
            get { return _Pass; }
        }

        public static Assertion PassWithWarning(WarningException warning)
        {
            return new Assertion()
            {
                HasPassed = true,
                Warning = warning
            };
        }

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
        public static Assertion Fail(IEnumerable<Exception> failures)
        {
            if (failures == null)
                throw new ArgumentNullException(nameof(failures));

            return Fail(new AggregateException(failures));
        }

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