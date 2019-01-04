using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThreeDGraph.json;

namespace VRGraph {
	public class Loader : MonoBehaviour {
		public String resource = "test";

		protected Game game;

		// Use this for initialization
		void Start () {
			TextAsset txt = Resources.Load(resource) as TextAsset;
			string json = txt.text;
			Graph g = new Graph();
			JsonUtility.FromJsonOverwrite(json, g);
			Debug.Log("Finished loading json. Nodes: " + g.nodes.length + ", Edges: " + g.edges.length);
			this.game = new Game();
			this.game.GenerateNodes(g);
		}

		void Update() {
			this.g.Update();
		}
	}
}
