using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRGraph.Utilities;

namespace VRGraph.GraphVisualisation
{
    public class Game<T>
    {
        private static System.Random random;
        public Dictionary<int, Node<T>> Nodes;
        public float Stability = 10;
        public static float DeltaTime = 0.3f;
        // public bool Stable => Nodes.Values.Sum(node => node.Force.LengthSquared()) < Stability;

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

        public Game(List<Tuple<int, int>> edges, Node<T>[] nodes)
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

        public Game(Dictionary<int, List<int>> edges, Node<T>[] nodes)
        {
            init(edges, nodes);
        }

        private void init(Dictionary<int, List<int>> edges, Node<T>[] nodes)
        {
            Nodes = nodes.ToDictionary(node => node.Id, node => node);
            foreach (KeyValuePair<int, List<int>> kvp in edges)
                Nodes[kvp.Key].InitEdges(Nodes, kvp.Value);
        }

        private void init(Dictionary<int, List<int>> edges, Dictionary<int, T> nodes)
        {
            Nodes = nodes.ToDictionary(kvp => kvp.Key, kvp => new Node<T>(kvp.Value, kvp.Key, generateMovableObject(nodes.Count, kvp.Key)));
            foreach (KeyValuePair<int, List<int>> kvp in edges)
                Nodes[kvp.Key].InitEdges(Nodes, kvp.Value);
        }

        private MovableObject generateMovableObject(int nodes, float location = -1)
        {
            if (random == null)
                random = new System.Random();
            System.Func<float> ry = () => random.Next(0, nodes);
            System.Func<float> r = () => random.Next(-nodes / 2, nodes / 2);
            Vector3 position = new Vector3(r(), r(), r());
            return new MovingObjectWithMaxForceBetweenNodes(
                    new MovingObjectWithResistance(
                    new MovingObject(position)));
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
