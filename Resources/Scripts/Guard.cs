using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

	// the player
	private GameObject bear;
	private Bear bearScript;

	// way points
	public GameObject[] waypoints;
	public GameObject[] suspWaypoints;
	private GameObject currentWaypoint;
	private Vector3 currentPosition;
	private int currentIndex;
	private int counter;
	private bool hasWayPoints;
	private bool isSuspicious;

	//rigidbody and guard model
	private Rigidbody rb;
	private GameObject model;

	// the detection circle
	private GameObject detectionCircle;

	// when guard is suspicious, and when to get back up after getting hit
	private float prevSuspicionTime;
	private const float suspicionCooldown = 0.3f;
	private const float getUpTime = 1f;

	// movement
	public float moveSpeed;
	private float minDistance = 0.2f;

	// how much a collision should increase suspicion by
	private const float suspicionIncreaseUponCollision = 10f;

	// audio
	private AudioSource[] audios;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		isSuspicious = false;
		counter = 0;
		// if this has waypoints, get waypoints
		hasWayPoints = (waypoints != null && waypoints.Length > 0);
		if(hasWayPoints)
		{
			currentWaypoint = waypoints[0];
			currentIndex = 0;
		}

		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();
		audios = GetComponents<AudioSource>();

		model = GetComponent<Transform>().Find("model").gameObject;

		detectionCircle = Instantiate ((GameObject)(Resources.Load("Prefabs/GuardDetectionCircle")));
	}


	//waypoints courtesy of http://www.attiliocarotenuto.com/83-articles-tutorials/unity/292-unity-3-moving-a-npc-along-a-path
	void MoveTowardWaypoint()
	{
		if(currentWaypoint == null)
			return;
		Vector3 direction = currentWaypoint.transform.position - this.transform.position;
		Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
		this.transform.position += moveVector;
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 4 * Time.deltaTime);

	}

	void changeWaypoints()
	{
		if (isSuspicious = true && counter == 0)
		{
			waypoints = suspWaypoints;
			
			hasWayPoints = (waypoints != null && waypoints.Length > 0);

			if(hasWayPoints)
			{
				currentWaypoint = waypoints[0];
				currentIndex = 0;
				counter += 1;
			}
		}
	}

	void checkSuspicion()
	{
		if (bearScript.isDiscovered)
		{
			model.GetComponent<Animator>().SetInteger("walkState",2);
			isSuspicious = true;
			changeWaypoints();
		}
	}

	// bear runs into this NPC
	void OnCollisionEnter(Collision collision)
	{	

		// bear run into guard
		if(collision.gameObject.name.Equals("Bear"))
		{
			if(Time.time - prevSuspicionTime > suspicionCooldown)
			{
				bearScript.IncreaseSuspicion(suspicionIncreaseUponCollision);
				audios[0].Play();
				prevSuspicionTime = Time.time;
				model.GetComponent<Animator>().SetInteger("walkState",2);
			}
		}
	}

	// put the detection circle where the guard is
	private void SetDetectionCirclePosition()
	{
		Vector3 v = GetComponent<Transform>().position;
		float y = GetComponent<Transform>().position.y - 1.08f;
		detectionCircle.GetComponent<Transform>().position = new Vector3(v.x, y, v.z);
	}
	
	// Update is called once per frame
	void Update () 
	{
		SetDetectionCirclePosition();
		checkSuspicion();
		

		// get back up after getting hit
		if(Time.time - prevSuspicionTime > getUpTime)
		{
			if(!hasWayPoints)
			{
				if(!isSuspicious)
				{
					model.GetComponent<Animator>().SetInteger("walkState",0);
				}
				GetComponent<Transform>().eulerAngles = Vector3.zero;
			}
			else if(!isSuspicious)
			{
				model.GetComponent<Animator>().SetInteger("walkState",1);
			}
		}
		
		if(counter >= 1 && hasWayPoints)
		{
			MoveTowardWaypoint();

			if ((Vector3.Distance (currentWaypoint.transform.position, transform.position) < minDistance) && (currentIndex <= (waypoints.Length -1))) {
				if (currentIndex >= (waypoints.Length - 1)){
					rb.velocity = new Vector3(0,0,0);
				}

				else{
					currentIndex += 1;
					currentWaypoint = waypoints [currentIndex];
				}
			}
		}	

		else if (counter == 0 && hasWayPoints)
		{

			MoveTowardWaypoint();

			if (currentWaypoint != null && Vector3.Distance (currentWaypoint.transform.position, transform.position) < minDistance) 
			{
				currentIndex += 1;
				if (currentIndex > waypoints.Length - 1)
				{
					currentIndex = 0;
				}
				currentWaypoint = waypoints [currentIndex];
			}
		}
	}
}
