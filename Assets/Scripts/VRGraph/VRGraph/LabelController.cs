using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRGraph.Json;
using VRGraph.GraphVisualisation;
using VRGraph.Utilities;
using NodeTester;
using TMPro;

namespace VRGraph {
	public class LabelController : MonoBehaviour {

        public string label;
        public bool labelShowsOnClick;
        public float labelShowsAfterTime = Mathf.Infinity;

        private TextMeshPro textMeshPro;

        void Start() {
            textMeshPro = gameObject.GetComponentInChildren<TextMeshPro>(true);
            Debug.Log(textMeshPro);
            textMeshPro.text = label;
        }

        void OnMouseDown()
        {
            Debug.Log("Clicked on object " + gameObject.name);
            if (labelShowsOnClick)
                showLabel();
        }

        void OnMouseEnter() {
            StopAllCoroutines();
            StartCoroutine(waitToShowLabel());
        }

        void OnMouseExit() {
            StopAllCoroutines();
            StartCoroutine(waitToHideLabel());
        }

        void showLabel() {
            Debug.Log("Label: "+ label);
            textMeshPro.gameObject.SetActive(true);
        }

        void hideLabel() {
            textMeshPro.gameObject.SetActive(false);
        }

        IEnumerator waitToShowLabel() {
            yield return new WaitForSeconds(labelShowsAfterTime);
            showLabel();
        }

        IEnumerator waitToHideLabel() {
            yield return new WaitForSeconds(labelShowsAfterTime);
            hideLabel();
        }
    }
}
