using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace VRTK {
    public class Item : NetworkBehaviour {
        public GameObject spawnObject;
        public GameObject placeholders;

        private float spawnDistance = 20;
        private VRTK_InteractableObject interactableObject;

        void Start() {
            interactableObject = GetComponent<VRTK_InteractableObject>();
        }

        // Update is called once per frame
        void Update() {
            float min = float.MaxValue;
            Transform minTransform = null;
            foreach (Transform child in placeholders.transform) {
                float distance = Vector3.Distance(transform.position, child.position);
                if (distance < min) {
                    min = distance;
                    minTransform = child;
                }
            }
            if (min < spawnDistance && !interactableObject.IsGrabbed()) {
                GameObject obj = Instantiate(spawnObject, minTransform.position, minTransform.rotation);
                NetworkServer.Spawn(obj);
                Destroy(minTransform.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
