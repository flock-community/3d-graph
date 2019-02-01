using System.Collections.Generic;
using UnityEngine;

namespace VRGraph.GraphVisualisation
{
    internal class MovingObjectWithAirResistance : MovingObjectWithExtraStuff
    {
        public readonly float Resistance;

        public MovingObjectWithAirResistance(MovableObject movableObject, float resistance = 0.5f) : base(movableObject)
        {
            Resistance = resistance;
        }

        public override void UpdateForce(IEnumerable<MovableObject> attractingNodes, IEnumerable<MovableObject> repellingNodes)
        {
            //TODO: Make air resistance better:
            //  - make sure if there is a big force, then the speed doesn't get too high
            //  - if the speed is too high, don't let the resistance do more then stand-still
            base.UpdateForce(attractingNodes, repellingNodes);
            Vector3 resistance = Resistance * Speed * Speed.magnitude;
        }
        public override void UpdateSpeed(float deltaTime)
        {
            base.UpdateSpeed(deltaTime);
        }
    }
}
