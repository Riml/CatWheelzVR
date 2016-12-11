using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerMovementMain : NetworkBehaviour {

    public float moveSpeed = 5.0f;
    //public float drag = 0.5f;
    //public float terminalRotationSpeed = 25.0f;

    public joystick joystick;
    //private Rigidbody thisrigidbody;
    //public Vector3 MoveVector { get; set; }
    // Use this for initialization
    public bool VRPlayer = false;
    private GlobalData globalData;
    private GameObject secondPlayer;

    // Use this for initialization
    void Start()
    {
        //MagnetSensor.OnCardboardTrigger += new MagnetSensor.CardboardTrigger(TempFunction);
        globalData = GameObject.Find("GlobalData").GetComponent<GlobalData>();
        VRPlayer = globalData.VRMode;

       
     

    }
	
	// Update is called once per frame
	private void Update () {

        if (VRPlayer)
        {
            if (!secondPlayer)
                FindSecondPlayer();
            else {
                this.transform.position = secondPlayer.transform.position;
                this.transform.rotation = secondPlayer.transform.rotation;
            }
        }
        else {
            var x = joystick.Horizontal() * Time.deltaTime * 150.0f;
            var z = joystick.Vertical() * Time.deltaTime * moveSpeed;

            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);
        }

        //MoveVector = PoolInput();

        //Move();
	}

    private void FindSecondPlayer() {

        GameObject[] playersObjectTemp = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in playersObjectTemp)
        {
            Debug.Log("lookinf for second player");
            if (p.GetComponent<NetworkIdentity>())
                if (!p.GetComponent<NetworkIdentity>().isLocalPlayer){
              
                        secondPlayer = p;
                }
        }

    }
    /*
   private void Move()
    {
        thisrigidbody.AddForce((MoveVector * moveSpeed));
    }
    private Vector3 PoolInput()
    {
        Vector3 dir = Vector3.zero;
        dir.x = joystick.Horizontal();
        dir.z = joystick.Vertical();
        if (dir.magnitude >1)
        {
            dir.Normalize();
        }
        return dir;
    }*/
}
