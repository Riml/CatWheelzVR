using UnityEngine;
using System.Collections;

public class PlayerSetup : MonoBehaviour {

    public Camera VRCamera;
    public GameObject reticle;
    public tank playerMovement;

    private GlobalData globalData;

    
    // Use this for initialization
	void Start () {
        globalData = GameObject.Find("GlobalData").GetComponent<GlobalData>();
        if (globalData.VRMode)
        {
            VRCamera.enabled = true;
            reticle.SetActive(true);
            globalData.GVR.SetActive(true);


        }
        else {
            playerMovement.enabled = true;
            playerMovement.joystick = GameObject.Find("Rbgknob").GetComponent<joystick>();

        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
