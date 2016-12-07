using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetManager : NetworkManager {

 	public int curPlayer;

	public GameObject vivePlayer;
	public GameObject cardboardPlayer;
	public Vector3 vivePlayerSpawn;
	public Vector3 cardboardPlayerSpawn;


	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId){
		Vector3 playerSpawnPos;
		GameObject playerPrefab;


		if (UnityEngine.Application.platform == RuntimePlatform.Android || true) {
			//remove camera. Viveplayer has own.
			playerPrefab = cardboardPlayer;
			playerSpawnPos = cardboardPlayerSpawn;


		} else {
			playerPrefab = vivePlayer;
			playerSpawnPos = vivePlayerSpawn;

		}

		GameObject player = (GameObject)GameObject.Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}

}
