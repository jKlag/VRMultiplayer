using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour {
	public int classNum = 1;

	public override void OnStartLocalPlayer()
	{
		CmdGetClass ();
		GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
		cam.GetComponent<PlayerController> ().setPlayer (gameObject);
		print (classNum);
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
		gameObject.GetComponent<Controller> ().setClassNum (NetworkServer.connections.Count);
	}

}

