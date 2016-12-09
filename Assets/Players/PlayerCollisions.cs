using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {

	public GameObject keyIcon;

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "Key") {
			GetComponent<PlayerController> ().getKey ();
			Destroy (col.gameObject);
			//keyIcon.transform.localScale = new Vector3 (.2f, .2f, .2f);
		} else if (col.gameObject.tag == "Spikes") {
			Camera.main.GetComponent<PlayerController> ().goToSpawn ();
		} else if (col.gameObject.tag == "Hitbox" ){
			//col.gameObject.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
			Camera.main.GetComponent<PlayerController> ().goToSpawn ();
		}
	}
}
