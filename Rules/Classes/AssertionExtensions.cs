// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Envivo.Fresnel.ModelTypes.Rules.Classes
{
    public static class AssertionExtensions
    {
        public static Assertion Consolidate(this IEnumerable<Assertion> assertions)
        {
            if (assertions == null)
                throw new ArgumentNullException(nameof(assertions));

            var failures = assertions.Where(a => a.HasFailed).ToList();
            var warnings = assertions.Where(a => a.HasWarning && !a.HasFailed).ToList();

            // Case 1: Failures exist
            if (failures.Count != 0)
            {
                var allFailures =
                    failures
                    .SelectMany(f =>
                        f.FailureException is AggregateException agg
                        ? agg.InnerExceptions.ToList()
                        : [f.FailureException]
                    )
                    .Where(e => e != null);

                return Assertion.Fail(allFailures);
            }

            // Case 2: Warnings exist (but no failures)
            if (warnings.Count != 0)
            {
                var allWarnings =
                    warnings
                    .Select(w => w.Warning)
                    .Where(e => e != null);

                var warningException = new WarningException("Multiple warnings were found", new AggregateException(allWarnings));
                return Assertion.PassWithWarning(warningException);
            }

            // Case 3: All passed
            return Assertion.Pass;
        }
    }
}

