using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DidWin : NetworkBehaviour {

	[SyncVar]
	public bool didWin;

	void Update () {
		if (didWin) {
			print ("You Win!");
		}
	}
}
