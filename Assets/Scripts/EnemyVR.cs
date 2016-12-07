using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemyVR : NetworkBehaviour {


    private bool inAim = false;
    
    [SyncVar]
    public bool dead = false;

    private bool veryDead = false;
    private GlobalData globalData;

    // Use this for initialization
    void Start() {

        globalData = GameObject.Find("GlobalData").GetComponent<GlobalData>();
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        //add handler when player will trigger button
        MagnetSensor.OnCardboardTrigger += new MagnetSensor.CardboardTrigger(EnemyInteraction);

    }



    // Update is called once per frame
    void Update() {
        if (dead && !veryDead)
        {
            veryDead = true;
            SoDead();

        }


    }

    //Triggered by PointerEnter
    public void InPlayersAim()
    {
        inAim = true;
        if (!dead)
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    //Triggered by PointerExit
    public void OutOfPlayersAim()
    {
        inAim = false;
        if (!dead)
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    public void EnemyInteraction() {

        if (inAim) {

            Network.Destroy(GetComponent<NetworkView>().viewID);
        }
    }  
      

    public void SoDead() {
        this.gameObject.GetComponent<Renderer>().material.color = Color.grey;

    }
}
