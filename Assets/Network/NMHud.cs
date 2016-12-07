using System;
using System.ComponentModel;

namespace UnityEngine.Networking
{
	public class NMHud : MonoBehaviour
	{
		public NetworkManager manager;

		// Runtime variable
		public String ipAddress;
		bool m_ShowServer;
		string address;
		public GUIStyle style;

		void Start(){

			if (ipAddress == "") {
				print ("Please Enter Host's IP in NMHUD component on Network GameObject");
			}

			if (UnityEngine.Application.platform == RuntimePlatform.Android || true) {
				//remove camera. Viveplayer has own.
				connectAndroid();

			} else {
				connectVive ();
			}

		}

		void Awake()
		{
			manager = GetComponent<NetworkManager>();
		}




		//shows gui for android device. This will just be a text box to enter an ip address and connect.
		void connectAndroid(){
			//before connection
			if(!manager.IsClientConnected ()) {
				manager.networkAddress = ipAddress;
				manager.StartClient ();
			}
			print (manager.IsClientConnected ());
		}

		//shows gui for htc vive player. This will just be the option for them to host
		void connectVive(){
			
			manager.StartHost();


		}
	}
}
