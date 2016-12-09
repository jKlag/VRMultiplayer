using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemyController : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent navMeshAgent;
	public float awarenessRadius;
	public float attackRadius;
	bool following = false;
	bool attacking = false;
	GameObject closestPlayer;
	public float playerScanWait;

	NetworkAnimator netAnim;

	public float attackRate;
	private float nextAttack;

	public GameObject damagePlane;
	bool dying;

	Animator animator;

	// Use this for initialization
	void Start () {
		navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		StartCoroutine (ScanForPlayers ());
		animator = GetComponent<Animator> ();
		netAnim = GetComponent<NetworkAnimator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (closestPlayer != null) {
			following = true;
			navMeshAgent.destination = closestPlayer.transform.position;
			transform.LookAt (closestPlayer.transform);
			if (Vector3.Distance (transform.position, closestPlayer.transform.position) < attackRadius) {
				navMeshAgent.Stop ();
				animator.SetBool ("Walking", false);
				following = false;
				attacking = true;
				if (Time.time > nextAttack) {
					nextAttack = Time.time + attackRate;
					netAnim.SetTrigger ("Attack");
					Debug.Log(animator.GetParameter(0));
				}
			} else {
				navMeshAgent.Resume ();
				animator.SetBool ("Walking", true);
			}
		} else {
			animator.SetBool ("Walking", false);
			following = false;
		}
	}

	IEnumerator ScanForPlayers ()
	{
		while (true)
		{
			// get all players in overlap sphere
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, awarenessRadius);
			// if there are players in sphere
			if (hitColliders.Length > 0) {
				if (!following) { // choose new player to follow
					float closestDistance = 1000;
					if (hitColliders [0].gameObject.tag == "Player") {
						closestPlayer = hitColliders[0].gameObject;
						closestDistance = Vector3.Distance (transform.position, closestPlayer.transform.position);
					}
					for (int i = 1; i < hitColliders.Length; i++) {
						if (hitColliders [i].gameObject.tag == "Player") {
							float distance = Vector3.Distance (transform.position, hitColliders [i].gameObject.transform.position);
							if(distance < closestDistance){
								closestDistance = distance;
								closestPlayer = hitColliders[i].gameObject;
							}
						}
					}
					if (this != null) {
						navMeshAgent.Resume ();
					}
				}
			}
			// wait to scan if already following somebody
			if (following) {
				yield return new WaitForSeconds (playerScanWait);
			} else {
				yield return new WaitForSeconds (0.1f);
			}
		}
	}
		
}
