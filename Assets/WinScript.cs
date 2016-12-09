using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class WinScript : NetworkBehaviour {
	[SyncVar]
	public bool playersWin;


	int playerCount = 0;
	// Use this for initialization

	void OnTriggerEnter(Collider col){
		int playerNum = GameObject.FindGameObjectsWithTag ("Player").Length;
		print (playerNum + " Players");
		if (col.gameObject.tag == "Player") {
			playerCount++;
		}

		if (playerNum == playerCount) {
			playersWin = true;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			playerCount--;
		}
	}
}
