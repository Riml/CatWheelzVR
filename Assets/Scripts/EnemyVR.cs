using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemyVR : NetworkBehaviour {


    private bool inAim = false;
    
    //[SyncVar(hook = "OnDeath")]
    public bool dead = false;

     private GlobalData globalData;

    // Use this for initialization
    void Start() {

        globalData = GameObject.Find("GlobalData").GetComponent<GlobalData>();
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        //add handler when player will trigger button
        MagnetSensor.OnCardboardTrigger += new MagnetSensor.CardboardTrigger(CmdEnemyInteraction);

    }



    // Update is called once per frame
    void Update() {
       


    }

    //Triggered by PointerEnter
    public void InPlayersAim()
    {
        inAim = true;
        if (!dead)
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>().ratToKill = this.gameObject;
    }

    //Triggered by PointerExit
    public void OutOfPlayersAim()
    {
        inAim = false;
        if (!dead)
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    //[Command]
    //public void CmdEnemyInteraction() {

    //    if (inAim) {
    //        dead = true;
    //        OnDeath(true);
    //        RatManager.DestroyRat(this.gameObject);
    //        //Network.Destroy(GetComponent<NetworkView>().viewID);
    //    }
    //}  

    public void CmdEnemyInteraction()
    {

        if (inAim)
        {
            dead = true;
            OnDeath(true);
            //RatManager.DestroyRat(GetComponent<NetworkInstanceId>());
            //Destroy(this);
        }
    }


    public void OnDeath(bool death) {
        dead = death;
        Debug.Log(dead);
    }
}
