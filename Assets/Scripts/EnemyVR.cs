﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemyVR : NetworkBehaviour {


    private bool inAim = false;
    
    //[SyncVar(hook = "OnDeath")]
    public bool dead = false;

    public GlobalData globalData;
    public AudioClip spawnSound;
    public AudioClip deathSound;

   

    // Use this for initialization
    void Start() {

        globalData = GameObject.Find("GlobalData").GetComponent<GlobalData>();
       // this.gameObject.GetComponent<Renderer>().material.color = Color.blue;

        this.gameObject.GetComponent<AudioSource>().PlayOneShot(spawnSound);
        globalData.IncreaseInfectationLevel();


    }



    // Update is called once per frame
    void Update() {
       


    }

    //Triggered by PointerEnter
    public void InPlayersAim()
    {
        //this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        inAim = true;
        if (!dead)
        {
            Debug.Log("Sending kill command");
           

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in players)
            {

                if (p.GetComponent<PlayerShooting>())
                    if (p.GetComponent<PlayerShooting>().VRPlayer)
                    {
                       
                        p.GetComponent<PlayerShooting>().KillRequest(this.GetComponent<NetworkIdentity>().netId);
                        

                    }
            }

            dead = true;
        }
       
    }

    //Triggered by PointerExit
    public void OutOfPlayersAim()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        inAim = false;
        
    }

}
