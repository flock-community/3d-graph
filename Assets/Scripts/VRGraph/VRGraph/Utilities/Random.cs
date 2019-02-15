using System;

namespace VRGraph.Utilities
{
    public class Random
    {
        public readonly int Seed;
        private int current;

        public Random(int seed)
        {
            Seed = seed;
        }
        public Random()
        {
            Seed = DateTime.Now.Millisecond;
        }

        public int Next()
        {
            return Next(int.MaxValue);
        }
        public int Next(int max)
        {
            current *= 29;
            current += 1;

            return Math.Abs(current % max);
        }
    }
}
