using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour {
	public GameObject vivePlayer;
	public Vector3[] spawnPoints;
	public GameObject[] classItems;
	public GameObject itemSpawnPlace;


	public int classNum = -1;

	public override void OnStartLocalPlayer()
	{

		if (GameObject.Find("VivePlayerInfo").GetComponent<VivePlayer>().isVivePlayer) {
			//vive
			Destroy (gameObject);
			GameObject x = Instantiate (vivePlayer);
			CmdReplaceMe (x);
			Destroy(GameObject.FindGameObjectWithTag ("MainCamera"));
			Destroy(GameObject.FindGameObjectWithTag ("GvrViewer"));


		} else {
			UnityEngine.AI.NavMeshAgent agent = gameObject.AddComponent<UnityEngine.AI.NavMeshAgent> ();
			agent.radius = .2f;
			agent.speed = 5f;
			GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
			cam.GetComponent<PlayerController> ().setPlayer (gameObject);
			setClass ();
			setWeapon ();
			movePlayer ();
		}
		
	}


		
	void setClass(){
		classNum = GameObject.FindGameObjectsWithTag ("Player").Length - 1;
	}

	void setWeapon(){
		GameObject weapon = Instantiate (classItems [classNum]);
		weapon.transform.parent = itemSpawnPlace.transform;
		weapon.transform.rotation = itemSpawnPlace.transform.rotation;
		weapon.transform.position = itemSpawnPlace.transform.position;
	}


	[Command]
	void CmdSpawnWep(GameObject weapon){
		NetworkServer.Spawn (weapon);
	}

	public void movePlayer(){
		Vector3 spawn;
		if (classNum >= 0 && classNum < spawnPoints.Length) {
			spawn = spawnPoints [classNum];

		} else {
			spawn = new Vector3(0,2,0);
		}
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerController> ().gotoPoint (spawn);
	}

	public void setClassNum(int x){
		classNum = x;
	}

	public int getClassNum(){
		return classNum;
	}

	public bool getIsLocalPlayer(){
		return isLocalPlayer;
	}




	[Command]
	void CmdReplaceMe(GameObject newPlayerObject)
	{
		NetworkServer.Spawn(newPlayerObject);
		NetworkServer.ReplacePlayerForConnection(connectionToClient, newPlayerObject, playerControllerId);

	}

}

