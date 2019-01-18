using System;
using System.Collections.Generic;
using System.Linq;
using VRGraph.Utilities;

namespace VRGraph.GraphVisualisation
{
    public class Game<T>
    {
        public static Dictionary<int, Node<T>> Nodes;
        private static Random random;
        public float Stability = 10;
        public static float DeltaTime = 0.3f;
        //public bool Stable => Nodes.Values.Sum(node => node.Force.LengthSquared()) < Stability;

        public Game(List<Tuple<int, int>> edges, Dictionary<int, T> nodes)
        {
            Dictionary<int, List<int>> edgesDict = new Dictionary<int, List<int>>();
            foreach (Tuple<int, int> edge in edges)
                if (edgesDict.ContainsKey(edge.Item1))
                    edgesDict[edge.Item1].Add(edge.Item2);
                else
                    edgesDict.Add(edge.Item1, new List<int> { edge.Item2 });
            init(edgesDict, nodes);
        }

        public Game(Dictionary<int, List<int>> edges, Dictionary<int, T> nodes)
        {
            init(edges, nodes);
        }

        private void init(Dictionary<int, List<int>> edges, Dictionary<int, T> nodes)
        {
            Nodes = nodes.ToDictionary(kvp => kvp.Key, kvp => new Node<T>(kvp.Value, kvp.Key, generateMovableObject(kvp.Key)));
            foreach (KeyValuePair<int, List<int>> kvp in edges)
                Nodes[kvp.Key].InitEdges(Nodes, kvp.Value);
        }

        private MovableObject generateMovableObject(float location = -1)
        {
            if (random == null)
                random = new Random();
            float r() => random.Next(0, 200) / 10000f;
            Vector3 position = new Vector3(r(), r(), r());
            return new MovingObjectWithResistance(new MovingObject(position));
        }

        public void Update()
        {
            foreach (int i in Nodes.Keys)
                Nodes[i].UpdateForce(Nodes.Values);
            foreach (int i in Nodes.Keys)
                Nodes[i].UpdatePosition(DeltaTime);
        }
    }
}
