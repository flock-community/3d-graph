using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRGraph.Json;
using VRGraph.GraphVisualisation;
using VRGraph.Utilities;

namespace VRGraph {
	public class Loader : MonoBehaviour {
		public string resource = "test";
		public GameObject nodePrefab;
		public float scaleFactor = 1f;

		Game<string> game;
		Dictionary<int, GameObject> nodes;

		void Start () {
			Graph g = new Graph();
			parseJson(g);
			Debug.Log("Finished parsing json. Nodes: " + g.nodes.Length + ", Edges: " + g.edges.Length);
			createGame(g);
			Debug.Log("Finished creating topology");
			render();
			Debug.Log("Finished rendering");
		}

		void Update() {
			this.game.Update();
			updatePositions();
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

			this.game = new Game<string>(edges, nodes);
		}

		private void render() {
			nodes = new Dictionary<int, GameObject>();
			foreach(Node<string> node in this.game.Nodes.Values) {
				GameObject obj = Instantiate(nodePrefab, convertVector(node.Position) / scaleFactor, Quaternion.identity);
				obj.name = ""+node.Id;
				nodes.Add(node.Id, obj);
			}
		}

		private void updatePositions() {
			foreach(Node<string> node in this.game.Nodes.Values) {
				nodes[node.Id].transform.position = convertVector(node.Position) / scaleFactor;
			}
		}

		private UnityEngine.Vector3 convertVector(VRGraph.Utilities.Vector3 pos) {
			return new UnityEngine.Vector3(pos.X, pos.Y, pos.Z);
		}
	}
}
