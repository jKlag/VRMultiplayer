using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ColorController : MonoBehaviour {

    public GameObject master;

    private ItemPlaceholder masterScript;
    private Renderer rend;

    // Use this for initialization
    void Start() {
        masterScript = master.GetComponent<ItemPlaceholder>();
        rend = GetComponent<Renderer>();
        rend.material.color = masterScript.highlightedColor.color;
    }

    // Update is called once per frame
    void Update() {
        rend.enabled = masterScript.highlighted;
    }
}