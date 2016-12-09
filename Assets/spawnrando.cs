using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class spawnrando : NetworkBehaviour {

	public GameObject spawnobj;
	public Vector3 pos;

	void Start(){
		GameObject inst = Instantiate (spawnobj);
		inst.transform.position = pos;

		NetworkServer.Spawn (inst);
	}
		
	// Update is called once per frame
	void Update () {
		
	}
}
