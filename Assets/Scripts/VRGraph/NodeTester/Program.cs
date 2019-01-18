using System;
using System.Collections.Generic;
using VRGraph.GraphVisualisation;

namespace NodeTester
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            Dictionary<int, int> nodes = new Dictionary<int, int>();
            edges.Add(new Tuple<int, int>(0, 1));
            edges.Add(new Tuple<int, int>(2, 1));
            edges.Add(new Tuple<int, int>(0, 3));
            nodes.Add(0, 0);
            nodes.Add(1, 1);
            nodes.Add(2, 2);
            nodes.Add(3, 3);

            Game<int> game = new Game<int>(edges, nodes);


            float numberOfUpdates = 10 / Game<string>.DeltaTime;
            for(int x = 0; x < numberOfUpdates; x++)
            {
                game.Update();
            }

        }
    }
}
