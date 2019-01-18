using System.Collections.Generic;
using VRGraph.GraphVisualisation;
using VRGraph.Utilities;

namespace NodeTester
{
    class Program
    {
        static void Main(string[] args)
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
            nodes.Add(0, 0);
            nodes.Add(1, 1);
            nodes.Add(2, 2);
            nodes.Add(3, 3);
            nodes.Add(4, 4);
            nodes.Add(5, 5);
            nodes.Add(6, 6);
            nodes.Add(7, 7);

            Game<int> game = new Game<int>(edges, nodes);


            float numberOfUpdates = 30 / Game<string>.DeltaTime;
            for(int x = 0; x < numberOfUpdates; x++)
                game.Update();
        }
    }
}
