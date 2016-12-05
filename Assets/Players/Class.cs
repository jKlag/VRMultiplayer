using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Class : NetworkBehaviour {

	public GameObject warriorItem;
	public GameObject detectiveItem;
	public GameObject destroyerItem;

	private string playerClass;

	Color warriorColor = Color.blue;
	Color detectiveColor = Color.yellow;
	Color destroyerColor = Color.red;
	int classNumber;

	[SyncVar]
	public Color m_color;



	public string getClass(){
		return playerClass;
	}

	public void setClass(int classNum){
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
		SetColor (warriorColor);
		placeItem (warriorItem);

	}
	void setDetective(){
		playerClass = "detective";
		SetColor (detectiveColor);
		placeItem (detectiveItem);

		//TODO:Dynamically attach warrior class
	}
	void setDestroyer(){
		playerClass = "destroyer";
		SetColor (destroyerColor);
		placeItem (destroyerItem);
		//TODO:Dynamically attach warrior class

	}

	void SetColor(Color c){
		CmdSetMeshColors (c);
	}

	void placeItem(GameObject theItem){
		GameObject emptyItem = GameObject.FindGameObjectWithTag("playerItem");
		GameObject item = Instantiate (theItem);
		item.transform.position = emptyItem.transform.position;
		item.transform.rotation = emptyItem.transform.rotation;
		item.transform.Rotate (new Vector3 (0, 90, 0));
		item.transform.parent = emptyItem.transform.parent;
		CmdSpawnItem (item);
	}

	[Command]
	public void CmdSpawnItem(GameObject item){
		NetworkServer.Spawn (item);

	}


	//Server Commands
	[Command]
	public void CmdSetMeshColors(Color c)
	{
		m_color = c;
		foreach (SkinnedMeshRenderer mesh in gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
		{
			mesh.material.color = m_color;
		}
		RpcSetColor (c);

	}

	[ClientRpc]
	void RpcSetColor( Color c )
	{
		m_color = c;
		foreach (SkinnedMeshRenderer mesh in gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
		{
			mesh.material.color = m_color;
		}
	}



	public void attack(RaycastHit hit){
		print ("attacking " + hit.collider.name);
		//TODO: Logic for classes attacking different colliders
	}


}
