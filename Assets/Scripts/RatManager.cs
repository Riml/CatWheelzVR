using UnityEngine;
using System.Collections;

public class RatManager : MonoBehaviour {

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
		Instantiate (rat, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
}
