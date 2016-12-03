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

		

		gameObject.GetComponent<Class>().setClass(chooseClassNum());

		print ("called");
		foreach(SkinnedMeshRenderer s in GetComponentsInChildren<SkinnedMeshRenderer>()){
				s.material.color = Color.blue;
		}
		GameObject cam = Instantiate (camera);
		Instantiate (gvrPrefab);

		cam.GetComponent<PlayerController> ().setPlayer (gameObject);

	}

	public bool getIsLocalPlayer(){
		return isLocalPlayer;
	}

}

