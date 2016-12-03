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

	public GameObject markerObject;
	GameObject marker;

	public GameObject swordCursor;
	public GameObject gvrCursor;

	public GameObject keyIcon;
	bool monsterDying = false;



	// Use this for initialization
	void Start () {
		marker = (GameObject)Instantiate (markerObject);
		player = GameObject.Find("Player(Clone)");
		animator = player.GetComponent<Animator> ();
		netAnim = player.GetComponent<NetworkAnimator> ();
		navMeshAgent = player.GetComponent<NavMeshAgent> ();
		transform.position = player.transform.position + new Vector3(0,1.2f,0) + .3f*player.transform.forward;
		float yRot = transform.rotation.eulerAngles.y;
		player.transform.eulerAngles = new Vector3 (0, yRot, 0);

	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat("Forward", Vector3.Magnitude(navMeshAgent.velocity), 0.1f, Time.deltaTime);
		float yRot = transform.rotation.eulerAngles.y;
		player.transform.eulerAngles = new Vector3 (0, yRot, 0);
		transform.position = player.transform.position + new Vector3(0,1.2f,0) + .3f*player.transform.forward;
		ray.origin = transform.position;
		ray.direction = transform.forward;

		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 50))
		{
			marker.transform.position = new Vector3 (hit.point.x, 0, hit.point.z);
		}
		if (Physics.Raycast (ray, out hit, 50)) {
			if (hit.collider.gameObject.tag == "monster") {
				swordCursor.transform.localScale = new Vector3 (0.05f, 0.05f, 0.05f);
				gvrCursor.transform.localScale = new Vector3 (0, 0, 0);
			} else {
				gvrCursor.transform.localScale = new Vector3 (1, 1, 1);
				swordCursor.transform.localScale = new Vector3 (0, 0, 0);
			}
		}

		if (Input.GetButtonDown ("Fire1")) 
		{
			netAnim.SetTrigger ("Attack");
			if (Physics.Raycast(ray, out hit,50))
			{
				if (hit.collider.gameObject.tag == "monster" && Time.time > nextAttack) {
					//play animation

					nextAttack = Time.time + attackRate;
					if (Vector3.Distance (transform.position, hit.point) < 1) {
						Destroy (hit.collider.gameObject);
					}
				} else {
					walking = true;
					navMeshAgent.destination = hit.point;
					navMeshAgent.Resume ();
					//marker.transform.position = hit.point;
				}
			}
		}


		/*if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
			if (!navMeshAgent.hasPath || Mathf.Abs (navMeshAgent.velocity.sqrMagnitude) < float.Epsilon)
				walking = false;
		} else {
			walking = true;
		}*/
		
		ray.origin = transform.position;
		ray.direction = transform.forward;

		/*if (Input.GetButtonDown ("Fire1")) 
		{
			if (Physics.Raycast (ray, out hit, 30)) {
				// not looking at ground or ceiling
				/*if (Physics.Raycast (ray, out hit, 5)) {
					if (hit.collider.gameObject.tag == "monster") {
						Destroy (hit.collider.gameObject);
					}
				}
				if (hit.point.y > 1f && hit.point.y < 10f && Time.time > nextAttack) {
					//play animation
					animator.SetTrigger("Attack3Trigger");
					nextAttack = Time.time + attackRate;
				}
				
				else if(hit.point.y >= 10f && Time.time > nextShout){
					//play shout
					nextShout = Time.time + shoutRate;
				}
			}
			else if(Time.time > nextAttack){
				animator.SetTrigger("Attack3Trigger");
				nextAttack = Time.time + attackRate;
				//play animation
			}
			if (mClass == PClass.sword && Physics.Raycast(ray, out hit,2))
			{
				if (hit.collider.gameObject.tag == "monster" && Time.time > nextAttack) {
					Destroy (hit.collider.gameObject);
					animator.SetTrigger("Attack3Trigger");
					nextAttack = Time.time + attackRate;
				}
			}
			if (mClass == PClass.hammer && Physics.Raycast(ray, out hit,2))
			{
				if (hit.collider.name == "bricks" && Time.time > nextAttack) {
					Destroy (hit.collider.gameObject);
					nextAttack = Time.time + attackRate;
				}
			}
		}*/
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
