using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
