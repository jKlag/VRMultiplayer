using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraCoord : MonoBehaviour {

	public GameObject buttonText;

	// Use this for initialization
	void Start () {


		if (UnityEngine.Application.platform != RuntimePlatform.Android || true) {
			//remove camera
			Destroy(GameObject.Find("GvrViewerMain"));
			Destroy(GameObject.Find("EventSystem"));

		}
	}

}
