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

	private GameObject cam;

	Animator animator;
	NetworkAnimator netAnim;

	private bool walking;

	private Vector3 camPosition;

	public GameObject keyIcon;
	bool monsterDying = false;

	void Start(){
		player = gameObject;
		netAnim = player.GetComponent<NetworkAnimator> ();
		animator = player.GetComponent<Animator> ();
		navMeshAgent = player.GetComponent<NavMeshAgent> ();

		cam = GameObject.FindGameObjectWithTag ("MainCamera");
	}



	
	// Update is called once per frame
	void FixedUpdate () {
		//make sure player is lp. may not be nec
//		if (!player.GetComponent<Controller> ().getIsLocalPlayer ()) {
//			return;
//		}
		//move camera to playerhead
		camPosition = gameObject.transform.Find("camPlacer").transform.position;
		cam.transform.position = camPosition;

//		//turn player to camera
//		if (navMeshAgent.velocity == Vector3.zero) {
//			gameObject.transform.forward = cam.transform.forward;
//		}
		animator.SetFloat ("Forward", Vector3.Magnitude (navMeshAgent.velocity), 0.1f, Time.deltaTime);



		//on click
		if (Input.GetButtonDown ("Fire1")) 
		{
			ray.origin = cam.transform.position;
			ray.direction = cam.transform.forward;

			RaycastHit hit;
			print (navMeshAgent.destination);

			if (Physics.Raycast (ray, out hit, 100)) {
				print (hit.collider.name);
				if (hit.collider.gameObject.tag != "hittable") {
					navMeshAgent.SetDestination (hit.point);
					navMeshAgent.Resume ();
				} else {
					attack (hit);
				}
			} else {
				animator.SetTrigger ("Attack");
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
