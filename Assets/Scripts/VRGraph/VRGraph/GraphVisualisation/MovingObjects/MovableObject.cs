using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VRGraph.GraphVisualisation
{
    public abstract class MovableObject
    {
        public virtual Vector3 Position
        {
            get;
            internal set;
        }
        internal virtual Vector3 Speed
        {
            get;
            set;
        }
        internal virtual Vector3 Force
        {
            get;
            set;
        }

        public Vector3 GetRepellingForce(MovableObject other)
        {
            if (other == null || Position == other.Position)
                return default(Vector3);
            return getRepellingMagnitude(other) * (Position - other.Position);
        }
        internal abstract float getRepellingMagnitude(MovableObject other);
        public Vector3 GetAttractingForce(MovableObject other)
        {
            if (other == null)
                return default(Vector3);
            return getAttractingMagnitude(other) * (Position - other.Position);
        }
        internal abstract float getAttractingMagnitude(MovableObject other);
        public virtual void UpdateForce(IEnumerable<MovableObject> attractingNodes, IEnumerable<MovableObject> repellingNodes)
        {
            Force = repellingNodes.Sum(node => GetRepellingForce(node)); 
            Force -= attractingNodes.Sum(node => GetAttractingForce(node));
        }
        public virtual void UpdateSpeed(float deltaTime)
        {
            Speed += Force * deltaTime;
        }
        public virtual void UpdatePosition(float deltaTime)
        {
            Position += Speed * deltaTime;
        }
        public override bool Equals(object obj)
        {
            MovableObject movableObject = obj as MovableObject;
            if (movableObject == null)
                return base.Equals(obj);
            return Position == movableObject.Position;
        }
    }
}
