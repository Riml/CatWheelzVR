using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RatMovement : NetworkBehaviour{

	public float destArea;
	public float timeTillChangeDest;
	public float runAwayDist;
	public float runAwaySpeed;
	public float minIdle = 1;
	public float maxIdle = 10;
	public float minWalk = 2; 
	public float maxWalk = 5;

	public Transform animal; //the object with the animations or 'Animation' Component (in this case, the child gameObject)
	public string[] moveAnimation; //name of walk (or movement) animation | Most animals movement animation's are named "Walk", but for instance, if the duck was in water, you can change this name to "Swim", instead.
	public string[] idleAnimation; //assortment of 'idle' animation names to be played at random | This can be, standing, eating, looking around, etc. Provide the name of the animation, as a string(text), not the animationClip.

    private GlobalData globalData;
	private bool idle= false; //if the animal is walking or idle | used to determine animations and movement, you do not need to check or uncheck this box
	private float waitTime = 0; //random time calculated using min and max idle or walk ^ | this makes movements more dynamic, and greatly reduces the chance of sycronized movements among multiple of the same animal
	private float timer = 0; //timer | used to count time spent idle and time spent moving

	Transform targetDest;
	Vector3 rngDest;
	NavMeshHit pingPos;

	Transform player; 
	float distFromPlayer;
	float destinationTimer;
	NavMeshAgent nav;

	// Death animation
	public string deathAnimation; // Death animation name
	public bool kill;
	private bool dead;


	// Use this for initialization
	void Start () {
        globalData = GameObject.Find("GlobalData").GetComponent<GlobalData>();
		animal = transform.GetChild(0).transform;

        if (globalData.VRMode)
        {
            this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            return;

        }

        player = GameObject.FindGameObjectWithTag ("Player").transform;
		destinationTimer = timeTillChangeDest;
		nav = GetComponent <NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (globalData.VRMode)
            return;
        CheckAiStage ();
	}

	void CheckAiStage(){
		destinationTimer += Time.deltaTime;
		distFromPlayer = Vector3.Distance(transform.position, player.position);

		if (distFromPlayer > runAwayDist) {
			NewDecision ();
		}

		if (distFromPlayer <= runAwayDist){
			RunAway ();
		}
	
	}

	void Stroll(){
		if (destinationTimer >= timeTillChangeDest) {
			rngDest = transform.position + (Random.insideUnitSphere * destArea);
			NavMesh.SamplePosition (rngDest, out pingPos, destArea, -1);
			timer = 0;
		}

		nav.SetDestination(pingPos.position);
	}

	void RunAway(){
		transform.LookAt(player);
		transform.Rotate(0, 180, 0);
		transform.Translate(Vector3.forward * runAwaySpeed * Time.deltaTime);

		rngDest = (transform.position - player.position) + (Random.insideUnitSphere * destArea);
		NavMesh.SamplePosition (rngDest, out pingPos, destArea, -1);

		nav.SetDestination(pingPos.position);
	}

	void  RandomIdle (){
		//**RANDOM IDLE ANIMATION**
		//make sure there are idle animations to pick from
		if(idleAnimation.Length > 0) {
			//Get random idle animation
			int randomIdle= Random.Range(0, idleAnimation.Length); //choose a random idle animation

			//Play idle animation
			animal.GetComponent<Animation>().CrossFade(idleAnimation[randomIdle]); //play Idle animation
			animal.GetComponent<Animation>()[idleAnimation[randomIdle]].time = 0;
			animal.GetComponent<Animation>()[idleAnimation[randomIdle]].speed = 1; //set Idle animation speed to 1
			animal.GetComponent<Animation>()[idleAnimation[randomIdle]].wrapMode = WrapMode.Once; //play the Idle animation once
		}
		else {
			//if no idle animation(s) have been provided, animal will walk continuously
			NewDecision();
		}
	}

	void  NewDecision (){
		//reset timer to 0
		timer = 0;

		//if currently walking, stand idle
		if(idle == false) {
			idle = true; //animal is now idle
			waitTime = Random.Range(minIdle, maxIdle); //get new waitTime
			RandomIdle(); //play randomIdle
		}
		//else if idle, start walking
		else {
			Stroll ();
			//Play walk animation
			int randomMove= Random.Range(0, moveAnimation.Length);
			animal.GetComponent<Animation>().CrossFade(moveAnimation[randomMove]); //play walk animation
			animal.GetComponent<Animation>()[moveAnimation[randomMove]].speed = 1; //set walk animation speed to 1
			animal.GetComponent<Animation>()[moveAnimation[randomMove]].wrapMode = WrapMode.Loop; //loop the walk animation

			waitTime = Random.Range(minWalk, maxWalk); //get new waitTime
			idle = false; //animal is now walking
		}
	}
}
