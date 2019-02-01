

namespace VRGraph.GraphVisualisation
{
    class MovingObjectWithMaxSpeed : MovingObjectWithExtraStuff
    {
        private const float max = 0.001f;

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