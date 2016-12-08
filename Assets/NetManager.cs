using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetManager : NetworkManager {

	public GameObject vivePlayer;
	public GameObject cardboardPlayer;
	public Vector3 vivePlayerSpawn;
	public Vector3 cardboardPlayerSpawn;


	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId){
		Vector3 playerSpawnPos;
		GameObject playerPrefab;

		playerPrefab = vivePlayer;
		playerSpawnPos = vivePlayerSpawn;
		Destroy (GameObject.FindGameObjectWithTag("MainCamera"));
		Destroy (GameObject.FindGameObjectWithTag ("GvrViewer"));
		Destroy (GameObject.Find ("EventSystem"));


		GameObject player = (GameObject)GameObject.Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}

}