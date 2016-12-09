using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class spawnrando : NetworkBehaviour {

	public GameObject spawnobj;
	public Vector3 pos;

	private bool spawned = false;

		
	// Update is called once per frame
	void Update () {
		//not NetworkServer.connections.Count counts number of connections including localClient
		if (!spawned && NetworkServer.connections.Count > 1) {
			spawned = true;
			GameObject inst = Instantiate (spawnobj);
			inst.transform.position = pos;
			NetworkServer.Spawn (inst);
		}
	}
}
