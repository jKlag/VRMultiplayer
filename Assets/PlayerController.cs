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



	// Use this for initialization
	void Start () {

	}

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
	void Update () {
		//make sure player is lp
		if (!player.GetComponent<Controller> ().getIsLocalPlayer ()) {
			return;
		}



		animator.SetFloat ("Forward", Vector3.Magnitude (navMeshAgent.velocity), 0.1f, Time.deltaTime);
		float yRot = transform.rotation.eulerAngles.y;
		player.transform.eulerAngles = new Vector3 (0, yRot, 0);
		transform.position = player.transform.position + new Vector3 (0, 1.2f, 0) + .3f * player.transform.forward;
		ray.origin = transform.position;
		ray.direction = transform.forward;



		//on click
		if (Input.GetButtonDown ("Fire1")) 
		{
			RaycastHit hit;
	

			if (Physics.Raycast (ray, out hit, 50)) {
				if (hit.collider.gameObject.name == "Plane") {
					//walking = true;
					navMeshAgent.SetDestination (hit.point);
					navMeshAgent.Resume ();
					//marker.transform.position = hit.point;
				} else {
					//swing
					attack (hit);
				}
			} else {
				netAnim.SetTrigger ("Attack");
			}
		}


	
		
		ray.origin = transform.position;
		ray.direction = transform.forward;

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
