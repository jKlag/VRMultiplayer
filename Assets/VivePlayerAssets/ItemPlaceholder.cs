using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlaceholder : MonoBehaviour {

    public GameObject items;
    public bool highlighted;
    public Material highlightedColor;

    private float highlightDistance = 50;

	void Start () {
        highlighted = false;
	}

    void Update() {
        int numChildren = 0;
        bool shouldHighlight = false;
        foreach (Transform child in items.transform) {
            numChildren++;
            float distance = Vector3.Distance(transform.position, child.position);
            if (distance < highlightDistance) {
                shouldHighlight = true;
            }
        }
        highlighted = shouldHighlight;
    }
}
