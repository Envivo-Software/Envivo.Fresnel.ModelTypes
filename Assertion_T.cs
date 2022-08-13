using System;
using System.ComponentModel;

namespace Envivo.Fresnel.ModelTypes
{
    public class Assertion<T> : Assertion
    {
        public static new Assertion<T> Pass(T result)
        {
            var actionResult = new Assertion<T>()
            {
                HasPassed = true,
                Result = result
            };
            return actionResult;
        }

        public static Assertion<T> PassWithWarning(T result, WarningException warning)
        {
            return new Assertion<T>()
            {
                HasPassed = true,
                Warning = warning
            };
        }

        public static Assertion<T> Fail(T result, Exception failure)
        {
            if (failure == null)
                throw new ArgumentNullException(nameof(failure));

            return new Assertion<T>()
            {
                HasFailed = true,
                FailureException = failure
            };
        }

        public static Assertion<T> FailWithWarning(T result, Exception failure, WarningException warning)
        {
            if (failure == null)
                throw new ArgumentNullException(nameof(failure));

            return new Assertion<T>()
            {
                HasFailed = true,
                FailureException = failure,
                Warning = warning
            };
        }

        public T Result { get; private set; }
    }
}