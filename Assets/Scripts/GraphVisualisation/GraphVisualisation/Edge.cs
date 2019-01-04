using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphVisualisation
{
    public struct Edge<T>
    {
        public Node<T> From;
        public Node<T> To;

        public Node<T> OtherSide(Node<T> node)
        {
            if (From == node)
                return To;
            if (To == node)
                return From;
            throw new ArgumentException(node + " does not exist in this edge.");
        }
    }
}
