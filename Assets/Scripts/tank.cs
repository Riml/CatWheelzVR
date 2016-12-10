using UnityEngine;
using System.Collections;

public class tank : MonoBehaviour {

    public float moveSpeed = 5.0f;
        public float drag = 0.5f;
        public float terminalRotationSpeed = 25.0f;

    public joystick joystick;
    private Rigidbody thisrigidbody;
    public Vector3 MoveVector { get; set; }
	// Use this for initialization
	private void Start () {
        //thisrigidbody = gameObject.GetComponent<Rigidbody>();
        //thisrigidbody.maxAngularVelocity = terminalRotationSpeed;
        //thisrigidbody.drag = drag;

    }
	
	// Update is called once per frame
	private void Update () {

        var x = joystick.Horizontal() * Time.deltaTime * 150.0f;
        var z = joystick.Vertical() * Time.deltaTime * moveSpeed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        //MoveVector = PoolInput();

        //Move();
	}

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
    }
}
