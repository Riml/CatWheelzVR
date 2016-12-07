using UnityEngine;
using System.Collections;

public class RatHealth : MonoBehaviour {

    private EnemyVR networkScript;

	// Use this for initialization
	void Start () {
        networkScript = GetComponent<EnemyVR>();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void CheckNetworkStatus() {
        if (networkScript.dead == true) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision) {
        networkScript.dead = true;
    }
}
