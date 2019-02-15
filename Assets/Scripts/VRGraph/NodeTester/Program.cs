using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRGraph.GraphVisualisation;
using VRGraph.Utilities;

namespace NodeTester
{
    internal class Program
    {
        private static void Main(string[] args) => TestRandomNumbers(100000000);

        public static void DefaultTest()
        {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            Dictionary<int, int> nodes = new Dictionary<int, int>();
            edges.Add(new Tuple<int, int>(0, 1));
            edges.Add(new Tuple<int, int>(0, 2));
            edges.Add(new Tuple<int, int>(0, 3));
            edges.Add(new Tuple<int, int>(0, 4));
            edges.Add(new Tuple<int, int>(0, 5));
            edges.Add(new Tuple<int, int>(0, 6));
            edges.Add(new Tuple<int, int>(0, 7));
            edges.Add(new Tuple<int, int>(0, 8));
            edges.Add(new Tuple<int, int>(1, 2));
            edges.Add(new Tuple<int, int>(1, 4));
            edges.Add(new Tuple<int, int>(1, 5));
            edges.Add(new Tuple<int, int>(2, 3));
            edges.Add(new Tuple<int, int>(2, 6));
            edges.Add(new Tuple<int, int>(3, 4));
            edges.Add(new Tuple<int, int>(3, 7));
            edges.Add(new Tuple<int, int>(4, 8));
            edges.Add(new Tuple<int, int>(5, 6));
            edges.Add(new Tuple<int, int>(5, 8));
            edges.Add(new Tuple<int, int>(6, 7));
            edges.Add(new Tuple<int, int>(7, 8));
            nodes.Add(0, 0);
            nodes.Add(1, 1);
            nodes.Add(2, 2);
            nodes.Add(3, 3);
            nodes.Add(4, 4);
            nodes.Add(5, 5);
            nodes.Add(6, 6);
            nodes.Add(7, 7);
            nodes.Add(8, 8);

            Game<int> game = new Game<int>(edges, nodes);

            float numberOfUpdates = 30 / Game<string>.DeltaTime;
            for (int x = 0; x < numberOfUpdates; x++)
                game.Update();
            for (int x = 0; x < numberOfUpdates; x++)
                game.Update();
        }
        public static void CloseTest()
        {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            Dictionary<int, int> nodes = new Dictionary<int, int>();
            edges.Add(new Tuple<int, int>(0, 1));
            edges.Add(new Tuple<int, int>(0, 2));
            edges.Add(new Tuple<int, int>(0, 3));
            edges.Add(new Tuple<int, int>(1, 2));
            edges.Add(new Tuple<int, int>(1, 3));
            edges.Add(new Tuple<int, int>(2, 3));
            nodes.Add(0, 0);
            nodes.Add(1, 1);
            nodes.Add(2, 2);
            nodes.Add(3, 3);


            Game<int> game = new Game<int>(edges, nodes);

            float numberOfUpdates = 30 / Game<string>.DeltaTime;
            for (int x = 0; x < numberOfUpdates; x++)
                game.Update();
            for (int x = 0; x < numberOfUpdates; x++)
                game.Update();
        }
        public static void TestRandomRange(int iterations)
        {
            VRGraph.Utilities.Random r = new VRGraph.Utilities.Random();
            int max = (int)Mathf.Sqrt(iterations);
            for (int i = 0; i < iterations; i++)
            {
                int t = r.Next(max);
                if (!(t < max && t >= 0))
                    throw new System.Exception();
            }
        }
        public static void TestRandomNumbers(int iterations)
        {
            VRGraph.Utilities.Random r = new VRGraph.Utilities.Random();
            int[] buckets = new int[19];
            int max = buckets.Length;

            for (int i = 0; i < iterations; i++)
                buckets[r.Next(max)]++;

            float expected = iterations / (float)max;
            float chiSquared = buckets.Sum(i => Mathf.Pow(i - expected, 2) / expected) / max;
            if (chiSquared > 1)
                throw new System.Exception();
        }
    }
}
