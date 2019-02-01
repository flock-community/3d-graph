using System.Collections.Generic;
using VRGraph.GraphVisualisation;
using VRGraph.Utilities;

namespace NodeTester
{
    class Program
    {
        static void Main(string[] args)
        {
            CloseTest();
        }

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
            List<Node<int>> nodes = new List<Node<int>>();
            edges.Add(new Tuple<int, int>(0, 1));
            nodes.Add(new Node<int>(0, 0, new MovingObjectWithResistance(new MovingObject(Vector3.Zero))));
            nodes.Add(new Node<int>(1, 1, new MovingObjectWithResistance(new MovingObject(Vector3.One * float.Epsilon))));

            Game<int> game = new Game<int>(edges, nodes.ToArray());

            float numberOfUpdates = 30 / Game<string>.DeltaTime;
            for (int x = 0; x < numberOfUpdates; x++)
                game.Update();
            for (int x = 0; x < numberOfUpdates; x++)
                game.Update();
        }
    }
}
