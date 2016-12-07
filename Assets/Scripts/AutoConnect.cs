using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AutoConnect : NetworkBehaviour {

    public GameObject networkManager;
    
    // Use this for initialization
	void Start () {
        networkManager.GetComponent<NetworkManager>().StartClient();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
