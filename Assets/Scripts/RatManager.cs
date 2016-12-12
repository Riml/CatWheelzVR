using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RatManager : NetworkBehaviour {

	public float spawnTime;
	public Transform[] spawnPoints;
	public GameObject[] rats;
	public float minScale;
	public float maxScale;
    public int nameCounter=0;

	int spawnPointIndex;

	// Use this for initialization
	void Start () {
        //InvokeRepeating ("SpawnRat", spawnTime, spawnTime);
        StartCoroutine(SpawnRat());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator SpawnRat(){
        yield return new WaitForSeconds(spawnTime);
        if (isServer)
        {
			// Random spawn point
            spawnPointIndex = Random.Range(0, spawnPoints.Length);

			// Random rat prefab
			int r = Random.Range(0, rats.Length);
			spawnPointIndex = Random.Range(0, spawnPoints.Length);

			// Generate random rat size
			float ratScale = Random.Range (minScale, maxScale + 1f);

			// Instantiate rat
			GameObject enemy = (GameObject)Instantiate(rats[r], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

			// Increase counter
            nameCounter++;
            
			// Name rat
			enemy.name = "rat" + nameCounter;

			// Transform local scale
			enemy.transform.localScale += new Vector3(ratScale, ratScale, ratScale);

            NetworkServer.Spawn(enemy);

            if(spawnTime>1f)
                spawnTime -= 0.05f;
        }
      
        StartCoroutine(SpawnRat());
    }

    public void DestroyRat(NetworkInstanceId netID)
    {
        if (!isServer)
            return;
        GameObject go = NetworkServer.FindLocalObject(netID);
        NetworkServer.Destroy(go);
    }
}
