namespace VRGraph.Utilities
{
    public struct Tuple<T>
    {
        public readonly T Item1;

        public Tuple(T item1)
        {
            Item1 = item1;
        }


        public override string ToString()
        {
            return "Tuple <" + Item1 + ">";
        }
    }
    public struct Tuple<T1, T2>
    {
        public readonly T1 Item1;
        public readonly T2 Item2;

        public Tuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public override string ToString()
        {
            return "Tuple <" + Item1 + ", " + Item2 + ">";
        }
    }
    public struct Tuple<T1, T2, T3>
    {
        public readonly T1 Item1;
        public readonly T2 Item2;
        public readonly T3 Item3;

        public Tuple(T1 item1, T2 item2, T3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public override string ToString()
        {
            return "Tuple <" + Item1 + ", " + Item2 + ", " + Item3 + ">";
        }
    }
}
