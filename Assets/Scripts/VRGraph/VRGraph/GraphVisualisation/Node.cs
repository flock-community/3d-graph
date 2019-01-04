using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GraphVisualisation
{
    public class Node<T>
    {
        public readonly T Content;
        public Vector3 Position
        {
            get;
            private set;
        }
        private Vector3 Speed;
        private Vector3 Force;
        public readonly int Id;
        public readonly List<Edge<T>> Edges;
        private Node<T>[] Neighbours => Edges.Select(e => e.OtherSide(this)).ToArray();
        private readonly int[] edgeIds;

        public Node(T content, int id, int[] edgeIds)
        {
            Content = content;
            Id = id;
            this.edgeIds = edgeIds;
            Edges = new List<Edge<T>>();
        }

        public void Init(Node<T>[] nodes)
        {
            Node<T>[] neighbours = nodes.Where(node => edgeIds.Contains(node.Id)).ToArray();
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
        private float getRepellingMagnitude(Node<T> other)
        {
            return 1 / (Position - other.Position).LengthSquared();
        }
        public Vector3 GetAttractingForce(Node<T> other)
        {
            return getAttractingMagnitude(other) * (Position - other.Position);
        }
        private float getAttractingMagnitude(Node<T> other)
        {
            return (Position - other.Position).LengthSquared();
        }
        public void UpdateForce(Node<T>[] nodes)
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
