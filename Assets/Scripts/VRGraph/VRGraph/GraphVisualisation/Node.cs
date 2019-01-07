using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace VRGraph.GraphVisualisation
{
    public class Node<T>
    {
        public enum Feature { Mass }
        public List<Feature> Features
        {
            get;
            protected set;
        }
        public readonly T Content;
        public Vector3 Position
        {
            get;
            private set;
        }
        private Vector3 Speed;
        internal Vector3 Force;
        public readonly int Id;
        public readonly List<Edge<T>> Edges;
        private Node<T>[] Neighbours => Edges.Select(e => e.OtherSide(this)).ToArray();

        public Node(T content, int id)
        {
            Content = content;
            Id = id;
            Edges = new List<Edge<T>>();
            Features = null;
        }

        public void InitEdges(Dictionary<int, Node<T>> nodes, List<int> neighbourIds)
        {
            Node<T>[] neighbours = neighbourIds.Select(id => nodes[id]).ToArray();
            neighbours = neighbours.Where(node => !node.Neighbours.Contains(this)).ToArray();
            for (int x = 0; x < neighbours.Length; x++)
            {
                Edge<T> edge = new Edge<T>(this, neighbours[x]);
                Edges.Add(edge);
                neighbours[x].Edges.Add(edge);
            }
        }

        public Vector3 GetRepellingForce(Node<T> other)
        {
            return getAttractingMagnitude(other) * (Position - other.Position);
        }
        protected virtual float getRepellingMagnitude(Node<T> other)
        {
            return 1 / (Position - other.Position).LengthSquared();
        }
        public Vector3 GetAttractingForce(Node<T> other)
        {
            return getAttractingMagnitude(other) * (Position - other.Position);
        }
        protected virtual float getAttractingMagnitude(Node<T> other)
        {
            return (Position - other.Position).LengthSquared();
        }
        public void UpdateForce(IEnumerable<Node<T>> nodes)
        {
            Force = nodes.Sum(node => node.GetRepellingForce(this));
            Force += Neighbours.Sum(node => node.GetAttractingForce(this));
        }
        public void UpdateSpeed()
        {
            Speed += Force;
        }
        public void UpdatePosition()
        {
            Position += Speed;
        }
        public override bool Equals(object obj)
        {
            Node<T> node = obj as Node<T>;
            if (node == null)
                return base.Equals(obj);
            return Id == node.Id && Content.Equals(node.Content);
        }
    }
}
