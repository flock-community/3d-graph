

namespace VRGraph.GraphVisualisation
{
    internal class MovingObjectWithMaxSpeed : MovingObjectWithExtraStuff
    {
        private const float max = 10;

        public MovingObjectWithMaxSpeed(MovableObject movableObject) : base(movableObject)
        {
        }

        public override void UpdateSpeed(float deltaTime)
        {
            base.UpdateSpeed(deltaTime);
            if (Speed.sqrMagnitude > max * max)
                Speed = Speed / Speed.magnitude * max;
        }
    }
}