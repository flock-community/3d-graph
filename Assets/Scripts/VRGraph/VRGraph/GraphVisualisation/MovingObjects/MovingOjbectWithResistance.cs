using System;
using VRGraph.Utilities;

namespace VRGraph.GraphVisualisation
{
    class MovingObjectWithResistance : MovingObjectWithExtraStuff
    {
        public const float Resistance = 0.5f;

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
