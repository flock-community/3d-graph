using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphVisualisation
{
    class Game<T>
    {
        public static Node<T>[] Nodes;


        public void Update()
        {
            for(int i = 0; i<Nodes.Length;i++)
            {
                Nodes[i].UpdateForce(Nodes);
                Nodes[i].UpdateSpeed();
            }
            for (int i = 0; i < Nodes.Length; i++)
                Nodes[i].UpdatePosition();
        }
    }
}
