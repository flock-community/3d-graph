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
        public T Content;
        public Vector3 Position
        {
            get;
            private set;
        }
        private Vector3 Speed;
        private Vector3 Force;
        public int Id;
        public Edge<T>[] Edges;
        private Node<T>[] Neighbours => Edges.Select(e => e.OtherSide(this)).ToArray();

        public Vector3 GetRepellingForce(Node<T> other)
        {
            return getAttractingMagnitude(other) * (Position - other.Position);
        }
        private float getRepellingMagnitude(Node<T> other)
        {
            return 1/(Position - other.Position).LengthSquared();
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
