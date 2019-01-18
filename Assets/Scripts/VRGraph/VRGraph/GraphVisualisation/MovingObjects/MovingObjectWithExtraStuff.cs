using VRGraph.Utilities;

namespace VRGraph.GraphVisualisation
{
    public abstract class MovingObjectWithExtraStuff : MovableObject
    {
        public override Vector3 Position
        {
            get => movableObject.Position;
            internal set => movableObject.Position = value;
        }
        internal override Vector3 Speed
        {
            get => movableObject.Speed;
            set => movableObject.Speed = value;
        }
        internal override Vector3 Force
        {
            get => movableObject.Force;
            set => movableObject.Force = value;
        }

        protected readonly MovableObject movableObject;

        protected MovingObjectWithExtraStuff(MovableObject movableObject)
        {
            this.movableObject = movableObject;
        }

        internal override float getRepellingMagnitude(MovableObject other)
        {
            MovingObjectWithExtraStuff _other = other as MovingObjectWithExtraStuff;
            return movableObject.getRepellingMagnitude(_other.movableObject);
        }
        internal override float getAttractingMagnitude(MovableObject other)
        {
            MovingObjectWithExtraStuff _other = other as MovingObjectWithExtraStuff;
            return movableObject.getAttractingMagnitude(_other.movableObject);
        }
    }
}
