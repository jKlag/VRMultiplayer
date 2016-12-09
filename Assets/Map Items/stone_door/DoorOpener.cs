using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour {

	public GameObject door;
	public bool doorOpened = false;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player"
			&& !doorOpened) {

			door.GetComponent<Animation> ().Play ("in-snap-open");
			doorOpened = true;

		}
	}
}
