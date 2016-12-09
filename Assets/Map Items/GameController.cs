using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameController : MonoBehaviour {

	private int playerCount = 0;
	public int numberOfPlayers;

	//[SyncVar]
	public float timer = 300.0f;

	//[SyncVar]
	public bool gameOn = false;

	//[SyncVar]
	public bool playersWin = false;
	
	// Update is called once per frame
	void Update () {
		if (playerCount == numberOfPlayers) {
			playersWin = true; 
		}
		if (timer <= 0.0f) {
			timer = 0.0f;
			// LOSE
		} else {
			timer -= Time.deltaTime;
		}
	}
		
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			++playerCount;
		}
	}


	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			--playerCount;
		}
	}
}
