using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour {

    public GameObject laserPrefab;
    public GameObject laserGun;//from which point we will be shooting
    public GameObject myBody;
    public AudioClip deathSound;
    public ParticleSystem laser;
    //public Camera mCamera;

    //public NetworkInstanceId ratToKill;
    private GlobalData globalData;
    public bool VRPlayer=false;

    // Use this for initialization
    void Start () {
        //MagnetSensor.OnCardboardTrigger += new MagnetSensor.CardboardTrigger(TempFunction);
        globalData = GameObject.Find("GlobalData").GetComponent<GlobalData>();
        VRPlayer = globalData.VRMode;
        
    }
   
    [Command]
    public void CmdDestroyRat(NetworkInstanceId ratName)
    {
        GameObject go = NetworkServer.FindLocalObject(ratName);

        //this.gameObject.GetComponent<AudioSource>().PlayOneShot(deathSound);
        globalData.DecreaseInfectationLevel();
        laser.Play();
        go.GetComponent<Renderer>().material.color = Color.black;
        NetworkServer.Destroy(go);

        //just for visuals, maybe?
       // GameObject laser = (GameObject)Instantiate(laserPrefab, laserGun.transform.position, laserGun.transform.rotation);
       // NetworkServer.Spawn(laser);

        //NetworkServer.UnSpawn(go);
        //Network.Destroy(theRat);
        //Destroy(theRat);
        //NetworkServer.Destroy(theRat);

    }

    // Update is called once per frame
    void Update () {

    }

    public void KillRequest(NetworkInstanceId ratToKill) {

        if (!ratToKill.IsEmpty())
        {
            if (isLocalPlayer)
            {
                Debug.Log("Sending Kill Command to : " + ratToKill.ToString());
                laser.Play();
                CmdDestroyRat(ratToKill);


            }
        }
    }
}
