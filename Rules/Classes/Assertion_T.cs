// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;
using System.ComponentModel;

namespace Envivo.Fresnel.ModelTypes.Rules.Classes
{
    /// <summary>
    /// Represents the result of a generic rule assertion, extending <see cref="Assertion"/> with a typed result value.
    /// </summary>
    /// <typeparam name="T">The type of the result value associated with this assertion.</typeparam>
    public class Assertion<T> : Assertion
    {
        /// <summary>
        /// Creates an <see cref="Assertion{T}"/> that has passed with the specified result value.
        /// </summary>
        /// <param name="result">The result value of type T.</param>
        /// <returns>An <see cref="Assertion{T}"/> indicating success with the provided result.</returns>
        public static new Assertion<T> Pass(T result)
        {
            var actionResult = new Assertion<T>()
            {
                HasPassed = true,
                Result = result
            };
            return actionResult;
        }

        /// <inheritdoc cref="Assertion.PassWithWarning(WarningException)" />
        /// <param name="result">The result value of type T.</param>
        public static Assertion<T> PassWithWarning(T result, WarningException warning)
        {
            return new Assertion<T>()
            {
                HasPassed = true,
                Warning = warning,
                Result = result,
            };
        }

        /// <inheritdoc cref="Assertion.Fail(Exception)" />
        /// <param name="result">The result value of type T.</param>
        public static Assertion<T> Fail(T result, Exception failure)
        {
            if (failure == null)
                throw new ArgumentNullException(nameof(failure));

            return new Assertion<T>()
            {
                HasFailed = true,
                FailureException = failure,
                Result = result,
            };
        }

        /// <inheritdoc cref="Assertion.FailWithWarning(Exception, WarningException)" />
        /// <param name="result">The result value of type T.</param>
        public static Assertion<T> FailWithWarning(T result, Exception failure, WarningException warning)
        {
            if (failure == null)
                throw new ArgumentNullException(nameof(failure));

            return new Assertion<T>()
            {
                HasFailed = true,
                FailureException = failure,
                Warning = warning,
                Result = result,
            };
        }

        public T Result { get; protected set; }
    }
}