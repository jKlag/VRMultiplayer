using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Class : NetworkBehaviour {


	private string playerClass;

//	Color warriorColor = Color.blue;
//	Color detectiveColor = Color.yellow;
//	Color destroyerColor = Color.red;
	int classNumber;

	private GameObject item;
	public Color color;

	public GameObject warriorItem;
	public GameObject detectiveItem;
	public GameObject destroyerItem;
	//public GameObject emptyItem;

	public string getClass(){
		return playerClass;
	}

	public void setClass(int classNum){
		if (!isLocalPlayer){return;}

		print ("Called");
		classNumber = classNum;
		switch (classNumber) {
		case 1:
			setDetective ();
			break;
		case 2:
			setDestroyer ();
			break;
		case 3:
			setWarrior ();
			break;
		}
	}

	public int getClassNum(){
		return classNumber;
	}

	void setWarrior(){
		playerClass = "warrior";
		//SetColor (warriorColor);

	}
	void setDetective(){
		playerClass = "detective";
		//SetColor (detectiveColor);
	}
	void setDestroyer(){
		playerClass = "destroyer";
		//SetColor (destroyerColor);
	}

	void SetColor(Color c){
		color = c;
	}



//
//	[Command]
//	public void CmdCreateItem(string spawnitem, string g)//since i want client to tell server object position i need these parameters
//	{
//		GameObject item = (GameObject)GameObject.Instantiate (spawnitem);
//		item.transform.parent = g.transform;
//		NetworkServer.Spawn(item);
//		RpcSyncBlockOnce (item.transform.localPosition, item.transform.localRotation, item, item.transform.parent.gameObject);
//	}
//
//	[ClientRpc]
//	public void RpcSyncBlockOnce(Vector3 localPos, Quaternion localRot, GameObject block, GameObject parent)
//	{
//		block.transform.parent = parent.transform;
//		block.transform.localPosition = localPos;
//		block.transform.localRotation = localRot;
//	}

//	//Server Commands
//	[Command]
//	public void CmdSpawnWarrior()
//	{
//		item = Instantiate (m_warriorItem);
//		item.transform.position = m_emptyItem.transform.position;
//		item.transform.rotation = m_emptyItem.transform.rotation;
//		item.transform.Rotate (new Vector3 (0, 90, 0));
//		item.transform.parent = m_emptyItem.transform.parent;
//		NetworkServer.Spawn (item);
//	}
//

	[Command]
	public void CmdSetColor(Color color){
		foreach (SkinnedMeshRenderer s in gameObject.GetComponentsInChildren<SkinnedMeshRenderer>()) {
			s.material.color = color;
		}
	}


	public void attack(RaycastHit hit){
		print ("attacking " + hit.collider.name);
		//TODO: Logic for classes attacking different colliders
	}


}
