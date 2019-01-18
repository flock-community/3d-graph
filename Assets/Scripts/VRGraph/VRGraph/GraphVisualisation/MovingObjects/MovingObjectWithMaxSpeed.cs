using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRGraph.GraphVisualisation
{
    class MovingObjectWithMaxSpeed : MovingObjectWithExtraStuff
    {
        private const float max = 10;

        public MovingObjectWithMaxSpeed(MovableObject movableObject) : base(movableObject)
        {
        }

        public override void UpdateSpeed(float deltaTime)
        {
            base.UpdateSpeed(deltaTime);
            if (Speed.LengthSquared() > max * max)
                Speed = Speed / Speed.Length() * max;
        }
    }
}