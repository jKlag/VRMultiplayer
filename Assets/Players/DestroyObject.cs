using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DestroyObject : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[Command]
	public void Cmd_DestroyThis(NetworkInstanceId netID)
	{
		GameObject theObject = NetworkServer.FindLocalObject(netID);
		NetworkServer.Destroy(theObject);
	}
}
