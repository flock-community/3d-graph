using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRGraph.Json;
using VRGraph.GraphVisualisation;
using VRGraph.Utilities;
using NodeTester;

namespace VRGraph {
	public class LabelController : MonoBehaviour {

        public string label;
        public bool labelShowsOnClick;
        public float labelShowsAfterTime = Mathf.Infinity;

        void OnMouseDown()
        {
            Debug.Log("Clicked on object " + gameObject.name);
            if (labelShowsOnClick)
                showLabel();
        }

        void OnMouseEnter() {
            StartCoroutine(waitToShowLabel());
        }

        void OnMouseExit() {
            StopAllCoroutines();
        }

        void showLabel() {
            Debug.Log("Label: "+ label);
        }

        IEnumerator waitToShowLabel() {
            yield return new WaitForSeconds(labelShowsAfterTime);
            showLabel();
        }
    }
}
