using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRGraph.GraphVisualisation
{
    public class NodeWithMass<T> : Node<T>
    {
        public readonly float Mass;

        public NodeWithMass(T content, int id, float mass) : base(content, id)
        {
            Mass = mass;
            if (Features == null)
                Features = new List<Feature>();
            Features.Add(Feature.Mass); 
        }

        protected override float getRepellingMagnitude(Node<T> other)
        {
            NodeWithMass<T> _other = other as NodeWithMass<T>;
            return _other != null ? base.getRepellingMagnitude(other) : _other.Mass * base.getRepellingMagnitude(_other);
        }
        protected override float getAttractingMagnitude(Node<T> other)
        {
            NodeWithMass<T> _other = other as NodeWithMass<T>;
            return _other != null ? base.getAttractingMagnitude(other) : _other.Mass * base.getAttractingMagnitude(_other);
        }
    }
}
