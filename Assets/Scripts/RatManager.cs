using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RatManager : NetworkBehaviour {

	public GameObject rat;  
	public float spawnTime;
	public Transform[] spawnPoints;
    public int nameCounter=0;

	int spawnPointIndex;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnRat", spawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnRat(){
        if (isServer)
        {
            spawnPointIndex = Random.Range(0, spawnPoints.Length);

            GameObject enemy = (GameObject)Instantiate(rat, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            nameCounter++;
            enemy.name = "rat" + nameCounter;

            NetworkServer.Spawn(enemy);
        }
    }

    public void DestroyRat(NetworkInstanceId netID)
    {
        if (!isServer)
            return;
        GameObject go = NetworkServer.FindLocalObject(netID);
        NetworkServer.Destroy(go);
    }
}
