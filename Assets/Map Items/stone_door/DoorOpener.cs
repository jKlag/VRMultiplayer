using UnityEngine;
using UnityEngine.Networking;

using System.Collections;

public class DoorOpener : NetworkBehaviour {

	public GameObject door;
	[SyncVar]
	public bool doorOpened = false;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && other.gameObject.GetComponent<Controller>().hasKey && !doorOpened) {

			door.GetComponent<Animation> ().Play ("in-snap-open");
			doorOpened = true;

		}
	}
}
