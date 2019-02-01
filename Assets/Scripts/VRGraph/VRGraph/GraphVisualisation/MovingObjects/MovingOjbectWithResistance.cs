using System;

namespace VRGraph.GraphVisualisation
{
    public class MovingObjectWithResistance : MovingObjectWithExtraStuff
    {
        public const float Resistance = 0.8f;

        public MovingObjectWithResistance(MovableObject movableObject) : base(movableObject)
        {
        }

        public override void UpdateSpeed(float deltaTime)
        {
            Speed *= (float)Math.Pow(1 - Resistance, deltaTime);
            base.UpdateSpeed(deltaTime);
        }
    }
}
