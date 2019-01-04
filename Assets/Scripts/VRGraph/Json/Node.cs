using ThreeDGraph.json;
using System;

namespace VRGraph.Json {
    [Serializable]
    public class Node {
        public string id;
        public string label;
        public string group;
        public int level;
        public float mass;
        public Meta meta;
    }
}
