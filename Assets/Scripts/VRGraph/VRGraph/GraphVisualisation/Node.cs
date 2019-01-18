using System.Collections.Generic;
using System.Linq;
using VRGraph.Utilities;

namespace VRGraph.GraphVisualisation
{
    public class Node<T>
    {
        private readonly MovableObject movableObject;
        public Vector3 Position => movableObject.Position;
        public readonly T Content;
        public readonly int Id;
        public readonly List<Edge<T>> Edges;
        private Node<T>[] Neighbours => Edges.Select(e => e.OtherSide(this)).ToArray();

        public Node(T content, int id, MovableObject movableObject)
        {
            Content = content;
            Id = id;
            Edges = new List<Edge<T>>();
            this.movableObject = movableObject;
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

        public void UpdatePosition(float deltaTime)
        {
            movableObject.UpdateSpeed(deltaTime);
            movableObject.UpdatePosition(deltaTime);
        }
        public void UpdateForce(IEnumerable<Node<T>> nodes)
        {
            movableObject.UpdateForce(Neighbours.Select(node => node.movableObject), nodes.Select(node => node.movableObject));
        }
        public override bool Equals(object obj)
        {
            Node<T> node = obj as Node<T>;
            if (node == null)
                return base.Equals(obj);
            return Id == node.Id && Content.Equals(node.Content);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
