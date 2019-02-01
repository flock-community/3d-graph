using System;

namespace VRGraph.GraphVisualisation
{
    class MovingObjectWithMaxForceBetweenNodes : MovingObjectWithExtraStuff
    {
        private const float max = 10;

        public MovingObjectWithMaxForceBetweenNodes(MovableObject movableObject)
            : base(movableObject)
        {
        }

        internal override float getRepellingMagnitude(MovableObject other)
        {
            MovingObjectWithMaxForceBetweenNodes _other = other as MovingObjectWithMaxForceBetweenNodes;
            return Math.Min(max, movableObject.getRepellingMagnitude(_other.movableObject));
        }
        internal override float getAttractingMagnitude(MovableObject other)
        {
            MovingObjectWithMaxForceBetweenNodes _other = other as MovingObjectWithMaxForceBetweenNodes;
            return Math.Min(max, movableObject.getRepellingMagnitude(_other.movableObject));
        }
    }
    class MovingObjectWithMaxTotal : MovingObjectWithExtraStuff
    {
        private const float max = 10;

        public MovingObjectWithMaxTotal(MovableObject movableObject)
            : base(movableObject)
        {
        }

        internal override float getRepellingMagnitude(MovableObject other)
        {
            MovingObjectWithMaxTotal _other = other as MovingObjectWithMaxTotal;
            return Math.Min(max, movableObject.getRepellingMagnitude(_other.movableObject));
        }
        internal override float getAttractingMagnitude(MovableObject other)
        {
            MovingObjectWithMaxTotal _other = other as MovingObjectWithMaxTotal;
            return Math.Min(max, movableObject.getRepellingMagnitude(_other.movableObject));
        }
    }
}
