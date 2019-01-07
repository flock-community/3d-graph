using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VRGraph.GraphVisualisation
{
    static class Extentions
    {
        public static Vector3 Sum<TIn>(this IEnumerable<TIn> iEnumerable, Func<TIn, Vector3> func)
        {
            return iEnumerable.Select(func).Aggregate((a, b) => a + b);
        }
        public static Vector3 Sum(this IEnumerable<Vector3> iEnumerable)
        {
            return iEnumerable.Aggregate((a, b) => a + b);
        }
    }
}
