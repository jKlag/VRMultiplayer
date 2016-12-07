using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour {

	public GameObject camera;
	public GameObject gvrPrefab;

	//chooses the class of the player incrementally. Allows for players to drop out and be readded.
	int chooseClassNum(){
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		bool unique = false;
		int num = 1;
		while (!unique) {
			int newNum = num;
			foreach(GameObject p in players){
				if (num == p.GetComponent<Class> ().getClassNum ()) {
					num++;
				}
			}
			if (newNum == num) {
				unique = true;
			}
		}
		print (num);
		return num;
	}
		

	public override void OnStartLocalPlayer()
	{
		
		GameObject ca = GameObject.Find ("Camera");
		if(ca != null){
			print ("Deactiv");
			Destroy(ca);

		}

		Class c = gameObject.GetComponent<Class> (); 

		c.setClass(chooseClassNum());
//
//
//		GameObject cam = Instantiate (camera);
//		GameObject gvr = Instantiate (gvrPrefab);
//
//
		//gameObject.transform.Find("Main Camera").GetComponent<PlayerController> ().setPlayer (gameObject);
//
	}

	public bool getIsLocalPlayer(){
		return isLocalPlayer;
	}

}

