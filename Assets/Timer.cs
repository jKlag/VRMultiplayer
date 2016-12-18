using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : NetworkBehaviour {
	[SyncVar] public float gameTime; //The length of a game, in seconds.
	[SyncVar] public float timer = -1; //How long the game has been running. -1=waiting for players, -2=game is done
	[SyncVar] public int minPlayers; //Number of players required for the game to start
	[SyncVar] public bool masterTimer = false; //Is this the master timer?
	[SyncVar] public bool playersCanMove = false; //can players move yet?
	//public ServerTimer timerObj;

	Timer serverTimer;

	void Start(){
		if(isServer){ // For the host to do: use the timer and control the time.
			if (isLocalPlayer) {
				serverTimer = this;
				masterTimer = true;
			}
		}else if(isLocalPlayer){ //For all the boring old clients to do: get the host's timer.
			Timer[] timers = FindObjectsOfType<Timer>();
			for(int i =0; i<timers.Length; i++){
				if(timers[i].masterTimer){
					serverTimer = timers [i];
				}
			}
		}
	}

	void Update(){
		if(masterTimer){ //Only the MASTER timer controls the time
			if(timer>=gameTime){
				timer = -2;
			}else if(timer == -1){
				//maybe add keyplaced to line 40
				if(NetworkServer.connections.Count >= minPlayers){
					playersCanMove = true;
					timer = 0;
				}
			}else if(timer == -2){
				//Game done.
				if (!!GameObject.Find ("Win").GetComponent<WinScript> ().playersWin) {
					print ("Vive wins!");
				}
			}else{
				timer += UnityEngine.Time.deltaTime;
			}
		}

		if(isLocalPlayer){ //EVERYBODY updates their own time accordingly.
			if (serverTimer) {
				gameTime = serverTimer.gameTime;
				timer = serverTimer.timer;
				minPlayers = serverTimer.minPlayers;
			} else { //Maybe we don't have it yet?
				Timer[] timers = FindObjectsOfType<Timer>();
				for(int i =0; i<timers.Length; i++){
					if(timers[i].masterTimer){
						serverTimer = timers [i];
					}
				}
			}
			if (!masterTimer && serverTimer.playersCanMove){
				gameObject.GetComponent<Controller> ().move = true;
			}

			if (GameObject.Find ("Win").GetComponent<WinScript> ().playersWin) {
				GameObject.Find ("Timer").GetComponent<Text> ().text = "You Win!";
				gameObject.GetComponent<Controller>().move = false;
			}else if (timer == -2 && !GameObject.Find("Win").GetComponent<WinScript>().playersWin) {
				GameObject.Find ("Timer").GetComponent<Text> ().text = "You Lose!";
				gameObject.GetComponent<Controller>().move = false;
			}else if (gameObject.tag == "Player") {
				GameObject.Find ("Timer").GetComponent<Text> ().text = 	((float)(Math.Round((double)(gameTime - timer), 2))).ToString ();
			}
		}
	}
}

