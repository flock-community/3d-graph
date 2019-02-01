using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VRGraph.Utilities
{
    internal static class Extentions
    {
        public static Vector3 Sum<TIn>(this IEnumerable<TIn> iEnumerable, Func<TIn, Vector3> func) => iEnumerable.DefaultIfEmpty().Select(func).Aggregate((a, b) => a + b);
        public static Vector3 Sum(this IEnumerable<Vector3> iEnumerable) => iEnumerable.Aggregate((a, b) => a + b);
    }
}
