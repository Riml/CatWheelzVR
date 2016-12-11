using UnityEngine;
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
        this.gameObject.GetComponent<Renderer>().material.color = Color.blue;

        this.gameObject.GetComponent<AudioSource>().PlayOneShot(spawnSound);
        globalData.IncreaseInfectationLevel();


    }



    // Update is called once per frame
    void Update() {
       


    }

    //Triggered by PointerEnter
    public void InPlayersAim()
    {
        inAim = true;
        if (!dead)
        {
           // this.gameObject.GetComponent<Renderer>().material.color = Color.red;

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in players)
            {

                if (p.GetComponent<PlayerShooting>())
                    if (p.GetComponent<PlayerShooting>().VRPlayer)
                    {
                        this.gameObject.GetComponent<AudioSource>().PlayOneShot(deathSound);
                        globalData.DecreaseInfectationLevel();
                        p.GetComponent<PlayerShooting>().KillRequest(this.GetComponent<NetworkIdentity>().netId);
                        

                    }
            }

            dead = true;
        }
       
    }

    //Triggered by PointerExit
    public void OutOfPlayersAim()
    {
        inAim = false;
        
    }

}
