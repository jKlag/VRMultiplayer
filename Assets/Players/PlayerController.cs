using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : MonoBehaviour {

	GameObject player;

	private NavMeshAgent navMeshAgent;

	private Ray ray;

	bool hasKey = false;

	enum PClass {hammer,sword,magnifier};

	PClass mClass = PClass.sword;

	public float attackRate;
	private float nextAttack;

	public float shoutRate;
	private float nextShout;

	Animator animator;
	NetworkAnimator netAnim;

	private bool walking;

	//public GameObject markerObject;
	//GameObject marker;

	public GameObject swordCursor;
	public GameObject gvrCursor;

	public GameObject keyIcon;
	bool monsterDying = false;



	//Sets the localplayer
	public void setPlayer(GameObject localPlayer){
		player = localPlayer;
		netAnim = player.GetComponent<NetworkAnimator> ();
		animator = player.GetComponent<Animator> ();
		navMeshAgent = player.GetComponent<NavMeshAgent> ();
		transform.position = player.transform.position + new Vector3(0,1.2f,0) + .3f*player.transform.forward;
		float yRot = transform.rotation.eulerAngles.y;
		player.transform.eulerAngles = new Vector3 (0, yRot, 0);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//make sure player is lp. may not be nec
		if (!player.GetComponent<Controller> ().getIsLocalPlayer ()) {
			return;
		}


		animator.SetFloat ("Forward", Vector3.Magnitude (navMeshAgent.velocity), 0.1f, Time.deltaTime);


		float yRot = transform.rotation.eulerAngles.y;
		//transform player position with camera position
		player.transform.eulerAngles = new Vector3 (0, yRot, 0);
		transform.position = player.transform.position + new Vector3 (0, 1.2f, 0) + .3f * player.transform.forward;


		//on click
		if (Input.GetButtonDown ("Fire1")) 
		{
			ray.origin = transform.position;
			ray.direction = transform.forward;

			RaycastHit hit;


			if (Physics.Raycast (ray, out hit, 50)) {
				if (hit.collider.gameObject.tag != "hittable") {
					navMeshAgent.SetDestination (hit.point);
					navMeshAgent.Resume ();
				} else {
					attack (hit);
				}
			} else {
				netAnim.SetTrigger ("Attack");
			}
		}


	
		
	}

	//plays attack animation and calls attack method in class
	void attack(RaycastHit hit){
		netAnim.SetTrigger ("Attack");
		nextAttack = Time.time + attackRate;
		player.GetComponent<Class> ().attack (hit);
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "key") {
			hasKey = true;
			Destroy (col.gameObject);
			keyIcon.transform.localScale = new Vector3 (.2f, .2f, .2f);
		}
		if (col.gameObject.tag == "fakekey") {
			if (mClass != PClass.magnifier) {
				hasKey = true;
				keyIcon.transform.localScale = new Vector3 (.2f, .2f, .2f);
			} 
			Destroy (col.gameObject);
		}
	}
		

}
