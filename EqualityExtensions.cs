using System;

namespace Envivo.Fresnel.ModelTypes
{
    public static class EqualityExtensions
    {
        /// <summary>
        /// Returns TRUE if the given objects are considered to be the same
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="getId">The function used to retrieve the ID from each object</param>
        /// <returns></returns>
        public static bool Equals<T>(this T a, object b, Func<T, Guid> getId)
            where T : class
        {
            if (b == null)
                return false;

            if (ReferenceEquals(a, b))
                return true;

            var bTyped = b as T;
            if (bTyped == null)
                return false;

            var idA = getId(a);
            var idB = getId(bTyped);

            return idA.Equals(idB);
        }

        /// <summary>
        /// Returns the hash code for the given object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="getId">The function used to retrieve the ID from the object</param>
        /// <returns></returns>
        public static int GetHashCode<T>(this T a, Func<T, Guid> getId)
            where T : class
        {
            var idA = getId(a);

            return idA == Guid.Empty ?
                a.GetHashCode() :
                idA.GetHashCode();
        }
    }
}