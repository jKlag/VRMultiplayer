using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerTime : MonoBehaviour {
	public GameObject TimeObject;
	private Timer timer;

	void Start(){
		timer = TimeObject.GetComponent<Timer> ();

	}

	void Update(){


	

		if (GameObject.Find ("Win").GetComponent<WinScript> ().playersWin) {
			GameObject.Find ("Timer").GetComponent<Text> ().text = "You Win!";
		}else if (timer.timer == -2 && !GameObject.Find("Win").GetComponent<WinScript>().playersWin) {
			GameObject.Find ("Timer").GetComponent<Text> ().text = "You Lose!";
		}else if (gameObject.tag == "Player") {
			GameObject.Find ("Timer").GetComponent<Text> ().text = 	((float)(Math.Round((double)(timer.gameTime - timer.timer), 2))).ToString ();
		}

	}



}
