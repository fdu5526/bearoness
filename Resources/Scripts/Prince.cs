using UnityEngine;
using System.Collections;

public class Prince : MonoBehaviour {

	// the player
	private GameObject bear;
	private Bear bearScript;

	// for patrolling
	private float randomWalkCoodown;
	private float prevStartWalkTime;
	private bool isStopped;
	private const float maxSpeed = 3f;
	private const float minStopTime = 0.3f;
	private const float maxStopTime = 2f;

	//waypoints
	public GameObject[] waypoints;
	private GameObject currentWaypoint;
	private int currentIndex;
	private bool hasWayPoints;

	//dance radius
	private GameObject danceCircle;

	//dance speed
	public float moveSpeed;
	private float minDistance = 0.2f;


	// for when player is too suspicious
	private const float runAwaySpeed = 20f;

	// how much a collision should increase suspicion by
	private const float suspicionIncreaseUponCollision = 10f;

	// audio
	private AudioSource[] audios;

	// Use this for initialization
	void Start () 
	{
		isStopped = Random.value >= 0.5f;
		prevStartWalkTime = 0f;

		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();
		audios = GetComponents<AudioSource>();

		// if this has waypoints, get waypoints
		hasWayPoints = (waypoints != null && waypoints.Length > 0);
		if(hasWayPoints)
		{
			currentWaypoint = waypoints[0];
			currentIndex = 0;
		}

		danceCircle = Instantiate ((GameObject)(Resources.Load("Prefabs/danceCircle")));
	}

	// bear runs into this NPC
	void OnCollisionEnter(Collision collision)
	{	
		// bear run into this NPC
		if(collision.gameObject.name.Equals("Bear"))
		{
			bearScript.IncreaseSuspicion(suspicionIncreaseUponCollision);
			audios[0].Play();
		}
	}

	void MoveTowardWaypoint()
	{
		Vector3 direction = currentWaypoint.transform.position - transform.position;
		Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
		transform.position += moveVector;
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 4 * Time.deltaTime);

	}

	// put the detection circle where the guard is
	private void SetDanceCirclePosition()
	{
		Vector3 v = GetComponent<Transform>().position;
		float y = GetComponent<Transform>().position.y - 1.08f;
		danceCircle.GetComponent<Transform>().position = new Vector3(v.x, y, v.z);
	}


	// there is a bear in the room omg run away ahhhhhhhhhhhhhhhhh
	void RunAway()
	{
		Vector3 tp = GetComponent<Transform>().position;
		Vector3 bp = bear.GetComponent<Transform>().position;
		
		// set direction and magnitude
		Vector3 d = tp - bp;
		d.Normalize();
		d *= runAwaySpeed;

		// gotta run
		GetComponent<Rigidbody>().velocity = d;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		SetDanceCirclePosition();
		if(!bearScript.isDiscovered)	// do ur dance gurl
		{
			MoveTowardWaypoint();

			if (Vector3.Distance (currentWaypoint.transform.position, transform.position) < minDistance) {
				currentIndex += 1;
				if (currentIndex > waypoints.Length - 1){
					currentIndex = 0;
				}
				currentWaypoint = waypoints [currentIndex];
			}
		}
		else					// run away if there is a bear
		{
			RunAway();
		}
		

	}
}
