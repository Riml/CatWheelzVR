﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    public Camera VRCamera;
    public GameObject reticle;
    public PlayerMovementMain playerMovement;
    public GameObject playerModel;
    private GlobalData globalData;
    public GameObject secondPlayer;
    public bool VRplayer;

    
    // Use this for initialization
	void Start () {
        StartCoroutine(FindSecondPlayer());
        globalData = GameObject.Find("GlobalData").GetComponent<GlobalData>();
        if (globalData.VRMode)
        {
            if (isLocalPlayer)
            {
                VRCamera.enabled = true;
                reticle.SetActive(true);
                globalData.GVR.SetActive(true);
              
            }


        }
        else {
            if (isLocalPlayer)
            {
               playerMovement.joystick = GameObject.Find("Rbgknob").GetComponent<joystick>();
            }
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator FindSecondPlayer()
    {


        GameObject[] playersObjectTemp = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in playersObjectTemp)
        {
            Debug.Log("lookinf for second player");
            if (p.GetComponent<NetworkIdentity>())
                if (!p.GetComponent<NetworkIdentity>().isLocalPlayer)
                {

                    secondPlayer = p;
                }
        }

        yield return new WaitForSeconds(0.5f);
        if (!secondPlayer)
            StartCoroutine(FindSecondPlayer());
        else {

            secondPlayer.GetComponent<PlayerSetup>().playerModel.SetActive(false);
            secondPlayer.GetComponent<PlayerSetup>().GetComponent<MeshRenderer>().enabled = false;

        }

    }
}
