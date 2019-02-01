using System.Collections.Generic;
using VRGraph.GraphVisualisation;
using VRGraph.Utilities;
using UnityEngine;

namespace NodeTester
{
    internal class Program
    {
        private static void Main(string[] args) => CloseTest();

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

        public static Game<string> GetGameWithXNodes(int x)
        {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            Dictionary<int, string> nodes = new Dictionary<int, string>();

            for(int i =0; i < x; i++)
                nodes.Add(i, ""+i);

            for(int i =0; i < x; i++) { 
                for(int j = i + 1; j < x; j++) {
                    if ((i + j) % 6 == 0)
                        edges.Add(new Tuple<int, int>(i, j));
                }
            }
            edges.Add(new Tuple<int, int>(0, 1));
            edges.Add(new Tuple<int, int>(2, 1));
            edges.Add(new Tuple<int, int>(3, 1));
            edges.Add(new Tuple<int, int>(4, 1));

            Game<string> game = new Game<string>(edges, nodes);

            return game;
        }
        public static void CloseTest()
        {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            List<Node<int>> nodes = new List<Node<int>>();
            edges.Add(new Tuple<int, int>(0, 1));
            nodes.Add(new Node<int>(0, 0, new MovingObjectWithResistance(new MovingObject(Vector3.zero))));
            nodes.Add(new Node<int>(1, 1, new MovingObjectWithResistance(new MovingObject(Vector3.one * float.Epsilon))));

            Game<int> game = new Game<int>(edges, nodes.ToArray());

            float numberOfUpdates = 30 / Game<string>.DeltaTime;
            for (int x = 0; x < numberOfUpdates; x++)
                game.Update();
            for (int x = 0; x < numberOfUpdates; x++)
                game.Update();
        }
    }
}
