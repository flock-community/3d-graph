using System;
using VRGraph.Utilities;

namespace VRGraph.GraphVisualisation
{
    public class MovingObject : MovableObject
    {
        public MovingObject(Vector3 position)
        {
            Position = position;
        }

        internal override float getRepellingMagnitude(MovableObject other)
        {
            MovingObject _other = other as MovingObject;
            if (_other == null)
                throw new ArgumentException("To use forces, please make sure this obj: " + this + " and the other " + other + " are of the same sub-type.");
            Vector3 difference = (Position - _other.Position);
            return difference.LengthSquared() == 0 ? 0 : 1 / difference.LengthSquared();
        }
        internal override float getAttractingMagnitude(MovableObject other)
        {
            return 1;
            if (other == null)
                return 0;
            MovingObject _other = other as MovingObject;
            if (_other == null)
                throw new ArgumentException("To use forces, please make sure this obj: " + this + " and the other " + other + " are of the same sub-type.");
            return (Position - _other.Position).LengthSquared();
        }
    }
}
