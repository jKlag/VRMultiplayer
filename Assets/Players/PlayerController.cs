using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : MonoBehaviour {

	GameObject player;

	private UnityEngine.AI.NavMeshAgent navMeshAgent;

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
	public float attackDistance;


	//Sets the localplayer
	public void setPlayer(GameObject localPlayer){
		player = localPlayer;
		netAnim = player.GetComponent<NetworkAnimator> ();
		animator = player.GetComponent<Animator> ();
		navMeshAgent = player.GetComponent<UnityEngine.AI.NavMeshAgent> ();
		transform.position = player.transform.position + new Vector3(0,2.8f,0) + .3f*player.transform.forward;
		float yRot = transform.rotation.eulerAngles.y;
		player.transform.eulerAngles = new Vector3 (0, yRot, 0);

	}

	void LateUpdate(){
		float yRot = transform.rotation.eulerAngles.y;
		if (player) {
			player.transform.eulerAngles = new Vector3 (0, yRot, 0);
			transform.position = player.transform.position + new Vector3 (0, 2.8f, 0) + .3f * player.transform.forward;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		animator.SetFloat("Forward", Vector3.Magnitude(navMeshAgent.velocity), 0.1f, Time.deltaTime);

		//make sure player is lp. may not be nec
		if (!player || !player.GetComponent<Controller> ().getIsLocalPlayer ()) {
			return;
		}
			

		if (Input.GetButtonDown ("Fire1")) 
		{
			ray.origin = transform.position;
			ray.direction = transform.forward;
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit,50))
			{
				Debug.Log (hit.collider.gameObject.tag);
				if (hit.collider.gameObject.tag == "Monster" && Time.time > nextAttack &&
					player.GetComponent<Controller>().getClassNum() == 0) {
					print ("attacking");
					//play animation
					netAnim.SetTrigger("Attack");
					nextAttack = Time.time + attackRate;
					if (Vector3.Distance (transform.position, hit.point) < attackDistance) {
						print("Kill");

						NetworkInstanceId monsterID = hit.collider.gameObject.GetComponent<NetworkIdentity> ().netId;
						player.GetComponent<DestroyObject> ().Cmd_DestroyThis (monsterID);
					}
				} 
				else if (hit.collider.gameObject.tag == "Brick" && Time.time > nextAttack &&
					player.GetComponent<Controller>().getClassNum() == 1) {
					//play animation
					netAnim.SetTrigger("Attack");
					nextAttack = Time.time + attackRate;
					if (Vector3.Distance (transform.position, hit.point) < attackDistance) {
						NetworkInstanceId brickID = hit.collider.gameObject.GetComponent<NetworkIdentity> ().netId;
						player.GetComponent<DestroyObject> ().Cmd_DestroyThis (brickID);
					}
				}
				else if (hit.collider.gameObject.tag == "Spikes" && Time.time > nextAttack &&
					player.GetComponent<Controller>().getClassNum() == 2) {
					//play animation
					netAnim.SetTrigger("Attack");
					nextAttack = Time.time + attackRate;
					if (Vector3.Distance (transform.position, hit.point) < attackDistance) {
						NetworkInstanceId trapID = hit.collider.gameObject.GetComponent<NetworkIdentity> ().netId;
						player.GetComponent<DestroyObject> ().Cmd_DestroyThis (trapID);
					}
				}
				else {
					walking = true;
					navMeshAgent.destination = hit.point;
					navMeshAgent.Resume ();
				}
			}
		}
	
		
	}


	public void getKey(){
		hasKey = true;
	}

	public bool getHasKey(){
		return hasKey;
	}

	public void gotoPoint(Vector3 x){
		navMeshAgent.enabled = false;
		player.transform.position = x;
		navMeshAgent.enabled = true;

		navMeshAgent.Stop ();
	}

	public void goToSpawn(){
		print ("Going to spawnpoint");
		// return to original spawnpoint
		gotoPoint(new Vector3(0,2,0));
	}

		

}
