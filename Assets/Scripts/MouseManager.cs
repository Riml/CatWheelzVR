using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MouseManager : NetworkBehaviour {

	public float spawnTime;
	public Transform[] spawnPoints;
	public GameObject[] rats;
	public float minScale;
	public float maxScale;

	int spawnPointIndex;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnRat", spawnTime, spawnTime);
	}

	// Update is called once per frame
	void Update () {

	}

	void SpawnRat(){
		// Random rat prefab
		int r = Random.Range(0, rats.Length);
		spawnPointIndex = Random.Range(0, spawnPoints.Length);

		// Generate random rat size
		float ratScale = Random.Range (minScale, maxScale + 1f);
		//float translateUp = tileHeight / 2f;

		// Instantiate rat
		GameObject enemy = (GameObject)Instantiate(rats[r], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

		// Transform local scale and translate tile appropriately
		enemy.transform.localScale += new Vector3(ratScale, ratScale, ratScale);
		//enemy.transform.position += new Vector3 (0, translateUp, 0);

		//NetworkServer.Spawn(enemy);
	}
}
