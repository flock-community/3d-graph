using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRGraph.Json;
using VRGraph.GraphVisualisation;
using VRGraph.Utilities;
using NodeTester;

namespace VRGraph {
	public class Loader : MonoBehaviour {
		public string resource = "test";
		public GameObject nodePrefab;
		public float scaleFactor = 1f;
		public float distanceFactor = 1f;

		public float maxDistanceFromCenter = 5000.0f;
		public int renderRandomNodes = 0;

		Game<string> game;
		Dictionary<int, GameObject> nodes;

		bool quit = false;

		void Start () {
			Graph g = new Graph();
			parseJson(g);
			Debug.Log("Finished parsing json. Nodes: " + g.nodes.Length + ", Edges: " + g.edges.Length);
			
			if(renderRandomNodes > 0)
				this.game = Program.GetGameWithXNodes(renderRandomNodes);
			else
				createGame(g);

			Debug.Log("Finished creating topology");
			render();
			Debug.Log("Finished rendering");
		}

		void Update() {
			if (!quit) {
				this.game.Update();
				updatePositions();
			}
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
				GameObject obj = Instantiate(nodePrefab, node.Position / distanceFactor, Quaternion.identity);
				obj.transform.localScale = obj.transform.localScale * scaleFactor;
				obj.name = ""+node.Id;
				nodes.Add(node.Id, obj);
			}
			
			//updatePositions();
		}

		private void updatePositions() {
			foreach(Node<string> node in this.game.Nodes.Values) {
				Vector3 targetPosition = node.Position / distanceFactor;
				nodes[node.Id].transform.position = Vector3.ClampMagnitude(targetPosition, maxDistanceFromCenter);
			}
			// First finish repositioning all nodes, then draw the edges
			foreach(Node<string> node in this.game.Nodes.Values) {
				int i = 0;
				GameObject source = nodes[node.Id];
				LineRenderer line = source.GetComponent<LineRenderer>();
				foreach(Edge<string> edge in node.Edges) {
					GameObject target = nodes[edge.To.Id];
					line.positionCount = i+2;
					line.SetPosition(i++, source.transform.position);
					line.SetPosition(i++, target.transform.position);
				}
			}
		}
	}
}
