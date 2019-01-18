using System.Collections.Generic;

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
            return Mass * _other.Mass * movableObject.getRepellingMagnitude(_other);
        }
        internal override float getAttractingMagnitude(MovableObject other)
        {
            MovingObjectWithMass _other = other as MovingObjectWithMass;
            return Mass * _other.Mass * movableObject.getAttractingMagnitude(_other);
        }
        public override void UpdateForce(IEnumerable<MovableObject> attractingNodes, IEnumerable<MovableObject> repellingNodes)
        {
            base.UpdateForce(attractingNodes, repellingNodes);
            Force /= Mass;
        }
    }
}
