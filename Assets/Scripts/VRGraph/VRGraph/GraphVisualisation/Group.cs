using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRGraph.GraphVisualisation
{
    class Group<T>
    {
        public readonly Dictionary<int, Node<T>> Nodes;

        public Group(Dictionary<int, Node<T>> nodes)
        {
            Nodes = nodes;
        }
    }
}
