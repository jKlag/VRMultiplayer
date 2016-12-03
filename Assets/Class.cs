using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Class : NetworkBehaviour {

	Color warriorColor = Color.blue;
	Color detectiveColor = Color.yellow;
	Color destroyerColor = Color.red;
	int classNumber;

	[SyncVar]
	public Color m_color;




	public void setClass(int classNum){
		print ("Called");
		classNumber = classNum;
		switch (classNumber) {
		case 1:
			setWarrior ();
			break;
		case 2:
			setDetective ();
			break;
		case 3:
			setDestroyer ();
			break;
		}
	}

	public int getClassNum(){
		return classNumber;
	}

	void setWarrior(){
		SetColor (warriorColor);
		//TODO:Dynamically attach warrior class
	}
	void setDetective(){
		SetColor (detectiveColor);
		//TODO:Dynamically attach warrior class
	}
	void setDestroyer(){
		SetColor (destroyerColor);
		//TODO:Dynamically attach warrior class

	}

	void SetColor(Color c){
		CmdSetMeshColors (c);
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
