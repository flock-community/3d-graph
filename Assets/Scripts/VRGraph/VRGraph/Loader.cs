using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphVisualisation;
using VRGraph.Json;

namespace VRGraph {
	public class Loader : MonoBehaviour {
		public String resource = "test";

		Game<String> game;

		void Start () {
			Graph g = new Graph();
			parseJson(g);
			Debug.Log("Finished parsing json. Nodes: " + g.nodes.length + ", Edges: " + g.edges.length);
			createGame(g);
		}

		void Update() {
			this.game.Update();
		}
		

		private void parseJson(Graph g) {
			TextAsset txt = Resources.Load(resource) as TextAsset;
			string json = txt.text;
			JsonUtility.FromJsonOverwrite(json, g);
		}

		private void createGame(Graph g) {
			Dictionary<int, string> nodes = new Dictionary<int, string>();
			Dictionary<string, int> ids = new Dictionary<string, int>();
			List<Tuple<int, int>> edges = new List<Tuple<int, int>>();

			foreach(Node n in g.nodes) {
				int id = nodes.Count;
				nodes.Add(id, n.label);
				ids.Add(n.id, id);
			}

			foreach(Edge e in g.edges) {
				Tuple<int, int> edge = new Tuple<int, int>(ids[e.from], ids[e.to]);
				edges.Add(edge);
			}

			this.game = new Game<String>(edges, nodes);
		}
	}
}
