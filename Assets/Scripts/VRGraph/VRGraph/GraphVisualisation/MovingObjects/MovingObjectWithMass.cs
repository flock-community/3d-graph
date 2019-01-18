using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VRGraph.GraphVisualisation
{
    public class MovingObjectWithMass : MovingObjectWithExtraStuff
    {
        public readonly float Mass;

        public MovingObjectWithMass(float mass, MovingObject movingObject) 
            : base(movingObject)
        {
            Mass = mass;
        }

        internal override float getRepellingMagnitude(MovableObject other)
        {
            MovingObjectWithMass _other = other as MovingObjectWithMass;
            return _other.Mass * movableObject.getRepellingMagnitude(_other);
        }
        internal override float getAttractingMagnitude(MovableObject other)
        {
            MovingObjectWithMass _other = other as MovingObjectWithMass;
            return _other.Mass * movableObject.getAttractingMagnitude(_other);
        }
    }
}
