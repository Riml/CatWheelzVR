﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RatMovement : NetworkBehaviour{

	public float destArea;
	public float timeTillChangeDest;
	public float runAwayDist;
	public float runAwaySpeed;

    private GlobalData globalData;

	Transform targetDest;
	Vector3 rngDest;
	NavMeshHit pingPos;

	Transform player; 
	float distFromPlayer;
	float timer;
	NavMeshAgent nav;


	// Use this for initialization
	void Start () {
        globalData = GameObject.Find("GlobalData").GetComponent<GlobalData>();

        if (globalData.VRMode)
        {
            this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            return;

        }

        player = GameObject.FindGameObjectWithTag ("Player").transform;
		timer = timeTillChangeDest;
		nav = GetComponent <NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (globalData.VRMode)
            return;
        CheckAiStage ();
	}

	void CheckAiStage(){
		timer += Time.deltaTime;
		distFromPlayer = Vector3.Distance(transform.position, player.position);

		if (distFromPlayer > runAwayDist) {
			Stroll ();
		}

		if (distFromPlayer <= runAwayDist){
			RunAway ();
		}
	
	}

	void Stroll(){
		if (timer >= timeTillChangeDest) {
			rngDest = transform.position + (Random.insideUnitSphere * destArea);
			NavMesh.SamplePosition (rngDest, out pingPos, destArea, -1);
			timer = 0;
		}

		nav.SetDestination(pingPos.position);
	}

	void RunAway(){
		transform.LookAt(player);
		transform.Rotate(0, 180, 0);
		transform.Translate(Vector3.forward * runAwaySpeed * Time.deltaTime);

		rngDest = (transform.position - player.position) + (Random.insideUnitSphere * destArea);
		NavMesh.SamplePosition (rngDest, out pingPos, destArea, -1);

		nav.SetDestination(pingPos.position);
	}
}
