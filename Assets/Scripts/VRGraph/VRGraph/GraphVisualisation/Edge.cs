using System;

namespace VRGraph.GraphVisualisation
{
    public struct Edge<T>
    {
        public Node<T> From;
        public Node<T> To;

        internal Edge(Node<T> from, Node<T> to)
        {
            From = from;
            To = to;
        }

        internal Node<T> OtherSide(Node<T> node)
        {
            if (From == node)
                return To;
            if (To == node)
                return From;
            throw new ArgumentException(node + " does not exist in this edge.");
        }
    }
}
