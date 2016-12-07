using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemySpawnSystem : NetworkBehaviour
{

    public GameObject enemyPrefab;

    // Use this for initialization
    void Start () {

        StartCoroutine(spawnEnemy());
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator spawnEnemy() {
        Vector3 spawnPosition = new Vector3(
               Random.Range(-8.0f, 8.0f),
               0.0f,
               Random.Range(-8.0f, 8.0f));

        GameObject enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        NetworkServer.Spawn(enemy);
        yield return new WaitForSeconds(5f);
        StartCoroutine(spawnEnemy());

        

    }
}
