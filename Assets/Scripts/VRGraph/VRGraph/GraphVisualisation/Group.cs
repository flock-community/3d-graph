using System.Collections.Generic;

namespace VRGraph.GraphVisualisation
{
    internal class Group<T>
    {
        public readonly Dictionary<int, Node<T>> Nodes;

        public Group(Dictionary<int, Node<T>> nodes)
        {
            Nodes = nodes;
        }
    }
}
