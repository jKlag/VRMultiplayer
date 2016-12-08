using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour {
	public GameObject vivePlayer;

	[SyncVar]
	public int classNum;

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
			CmdGetClass ();
			print (getClassNum());
			GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
			cam.GetComponent<PlayerController> ().setPlayer (gameObject);
		}
		
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
	void CmdGetClass(){
		foreach (NetworkConnection c in NetworkServer.connections) {
			print (c.ToString());
		}
		gameObject.GetComponent<Controller> ().setClassNum (NetworkServer.connections.Count);
	}



	[Command]
	void CmdReplaceMe(GameObject newPlayerObject)
	{
		NetworkServer.Spawn(newPlayerObject);
		NetworkServer.ReplacePlayerForConnection(connectionToClient, newPlayerObject, playerControllerId);

	}

}

