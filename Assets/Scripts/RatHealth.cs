using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RatHealth : NetworkBehaviour
{
    private EnemyVR networkScript;

	// Use this for initialization
	void Start () {
        networkScript = GetComponent<EnemyVR>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckNetworkStatus();
	}

    void CheckNetworkStatus() {
        if (networkScript.dead == true) {
            //NetworkServer.UnSpawn(gameObject);
            NetworkServer.Destroy(gameObject);
            Destroy(gameObject);
            //NetworkServer.UnSpawn(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision) {
        networkScript.dead = true;
    }

}
