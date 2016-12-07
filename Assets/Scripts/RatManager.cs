using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RatManager : NetworkBehaviour {

	public GameObject rat;  
	public float spawnTime;
	public Transform[] spawnPoints;

	int spawnPointIndex;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnRat", spawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnRat(){
		spawnPointIndex = Random.Range (0, spawnPoints.Length);

		GameObject enemy = (GameObject)Instantiate (rat, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        NetworkServer.Spawn(enemy);
    }
}
