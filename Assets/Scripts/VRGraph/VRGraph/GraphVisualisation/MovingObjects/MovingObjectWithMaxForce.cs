using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRGraph.GraphVisualisation
{
    class MovingObjectWithMaxForce : MovingObjectWithExtraStuff
    {
        private const float max = 10;

        public MovingObjectWithMaxForce(MovableObject movableObject)
            : base(movableObject)
        {
        }

        internal override float getRepellingMagnitude(MovableObject other)
        {
            MovingObjectWithMaxForce _other = other as MovingObjectWithMaxForce;
            return Math.Min(max, movableObject.getRepellingMagnitude(_other.movableObject));
        }
        internal override float getAttractingMagnitude(MovableObject other)
        {
            MovingObjectWithMaxForce _other = other as MovingObjectWithMaxForce;
            return Math.Min(max, movableObject.getRepellingMagnitude(_other.movableObject));
        }
    }
}
