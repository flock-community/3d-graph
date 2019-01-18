using System;
using System.Collections.Generic;
using System.Linq;

namespace VRGraph.Utilities
{
    static class Extentions
    {
        public static Vector3 Sum<TIn>(this IEnumerable<TIn> iEnumerable, Func<TIn, Vector3> func)
        {
            return iEnumerable.DefaultIfEmpty().Select(func).Aggregate((a, b) => a + b);
        }
        public static Vector3 Sum(this IEnumerable<Vector3> iEnumerable)
        {
            return iEnumerable.Aggregate((a, b) => a + b);
        }
    }
}
