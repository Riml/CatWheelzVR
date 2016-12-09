using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour {

    public GameObject laserPrefab;
    public GameObject laserGun;//from which point we will be shooting

    // Use this for initialization
    void Start () {
        MagnetSensor.OnCardboardTrigger += new MagnetSensor.CardboardTrigger(CmdBeamsOfDeath);

    }
    [Command]
    public void CmdBeamsOfDeath() {

        GameObject laser = (GameObject)Instantiate(laserGun, Vector3.zero, Quaternion.identity);
        NetworkServer.Spawn(laser);


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
