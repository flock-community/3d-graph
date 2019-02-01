using System;
using UnityEngine;

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
            Vector3 difference = Position - _other.Position;
            return difference.sqrMagnitude == 0 ? 0 : 1 / difference.magnitude;
        }
        internal override float getAttractingMagnitude(MovableObject other)
        {
            MovingObject _other = other as MovingObject;
            if (_other == null)
                throw new ArgumentException("To use forces, please make sure this obj: " + this + " and the other " + other + " are of the same sub-type.");
            Vector3 difference = Position - _other.Position;
            return difference.magnitude - 1;
        }
    }
}
