using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour {

    public GameObject laserPrefab;
    public GameObject laserGun;//from which point we will be shooting
    public GameObject myBody;
    public Camera mCamera;

    public GameObject ratToKill;

    // Use this for initialization
    void Start () {
        MagnetSensor.OnCardboardTrigger += new MagnetSensor.CardboardTrigger(CmdBeamsOfDeath);


        

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
    public void CmdDestroyRat(GameObject go)
    {
        Debug.Log("+++++++++++++++++++++++++++++" + go.name);
        EnemyVR enemy = go.GetComponent<EnemyVR>();
        enemy.CmdEnemyInteraction();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("isLocalPlayer" + isLocalPlayer);
            if (!isLocalPlayer)
                return;
            CmdBeamsOfDeath();
        }
    }
}
