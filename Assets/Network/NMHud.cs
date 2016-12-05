using System;
using System.ComponentModel;

#if ENABLE_UNET

namespace UnityEngine.Networking
{
	[AddComponentMenu("Network/NMHud")]
	[RequireComponent(typeof(NetworkManager))]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class NMHud : MonoBehaviour
	{
		public NetworkManager manager;
		[SerializeField] public bool showGUI = true;
		[SerializeField] public int offsetX;
		[SerializeField] public int offsetY;

		// Runtime variable
		bool m_ShowServer;
		string address;
		public GUIStyle style;

		void Awake()
		{
			manager = GetComponent<NetworkManager>();
		}

		void Update()
		{
			if (!showGUI)
				return;

			if (!manager.IsClientConnected() && !NetworkServer.active && manager.matchMaker == null)
			{
				if (UnityEngine.Application.platform != RuntimePlatform.Android)
				{

					if (Input.GetKeyDown(KeyCode.H))
					{
						manager.StartHost();
						address = NetworkManager.singleton.networkAddress;
						print (address);
						//showGUI = false;
					}
				}
				//DEBUG: Remove
				if (Input.GetKeyDown(KeyCode.C))
				{
					print (manager.networkAddress);
					manager.StartClient();
				}
				//End remove
			}
			if (NetworkServer.active && manager.IsClientConnected())
			{
				if (Input.GetKeyDown(KeyCode.X))
				{
					manager.StopHost();
				}
			}
		}

		void OnGUI()
		{
			//either on android or vive
			//if (UnityEngine.Application.platform == RuntimePlatform.Android) {
				showAndroid ();
			//} else {
				//showAndroid ();
				showVive ();
			//}
				
		}


		//shows gui for android device. This will just be a text box to enter an ip address and connect.
		void showAndroid(){
			//before connection
			if (!manager.IsClientConnected ()) {
				if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 10, 200, 50), "Connect To Host")) {
					manager.StartClient ();
				}
				manager.networkAddress = GUI.TextField (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 70, 200, 30), manager.networkAddress);
			}
		}

		//shows gui for htc vive player. This will just be the option for them to host
		void showVive(){
			bool noConnection = (manager.client == null || manager.client.connection == null ||
				manager.client.connection.connectionId == -1);
			

			//Case1: Vive is not yet hosting. Prompts player to host
			if (noConnection) {
				GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 10, 100, 50), "Press 'H' to Host", style);
			}else{
			//Case2: Vive is hosting. Should show player's local IP. Should allow them to end game.
				style.alignment = TextAnchor.MiddleLeft;
				string serverMsg = "Address =" + Network.player.ipAddress + " Port=" + manager.networkPort;
				GUI.Label (new Rect (20, 10, 300, 50), serverMsg, style);
				GUI.Label (new Rect (20, 60, 100, 50), "Press 'X' to Stop", style);
			}

		}
	}
}

#endif //ENABLE_UNET