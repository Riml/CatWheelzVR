using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour {

    public GameObject laserPrefab;
    public GameObject laserGun;//from which point we will be shooting
    public GameObject myBody;
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
    public void CmdBeamsOfDeath() {
      
        Debug.Log("isLocalPlayer2" + isLocalPlayer);
        /*RaycastHit vHit = new RaycastHit();
        Ray ray = mCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out vHit, 30))
        {
            EnemyVR enemy = null;
            enemy = vHit.collider.gameObject.GetComponent<EnemyVR>();
            if (enemy)
            {
                Debug.Log("------------------------------------" + vHit.collider.gameObject.name);
                CmdDestroyRat(vHit.collider.gameObject);
            }
        }*/

        //myBody.GetComponent<Renderer>().material.color = Color.yellow;
        GameObject laser = (GameObject)Instantiate(laserPrefab, laserGun.transform.position, laserGun.transform.rotation);
        NetworkServer.Spawn(laser);
        //CmdDestroyRat();

    }

    [Command]
    public void CmdDestroyRat(NetworkInstanceId ratName)
    {
        GameObject go = NetworkServer.FindLocalObject(ratName);
       
        go.GetComponent<Renderer>().material.color = Color.black;
        NetworkServer.Destroy(go);

        //NetworkServer.UnSpawn(go);
        //Network.Destroy(theRat);
        //Destroy(theRat);
        //NetworkServer.Destroy(theRat);
       
    }

    // Update is called once per frame
    void Update () {

    }

    public void TempFunction(NetworkInstanceId ratToKill) {

        if (!ratToKill.IsEmpty())
        {
            if (isLocalPlayer)
            {
                Debug.Log("Sending Kill Command to : " + ratToKill.ToString());
                CmdDestroyRat(ratToKill);


            }
        }
    }
}
