using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

	// the player
	private GameObject bear;
	private Bear bearScript;

	// way points
	public GameObject[] waypoints;
	private GameObject currentWaypoint;
	private int currentIndex;
	private bool hasWayPoints;

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
		hasWayPoints = (waypoints != null && waypoints.Length > 0);
		if(hasWayPoints)
		{
			currentWaypoint = waypoints[0];
			currentIndex = 0;
		}

		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();
		audios = GetComponents<AudioSource>();

		detectionCircle = Instantiate ((GameObject)(Resources.Load("Prefabs/GuardDetectionCircle")));
	}


	//waypoints courtesy of http://www.attiliocarotenuto.com/83-articles-tutorials/unity/292-unity-3-moving-a-npc-along-a-path
	void MoveTowardWaypoint()
	{
		Vector3 direction = currentWaypoint.transform.position - transform.position;
		Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
		transform.position += moveVector;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 4 * Time.deltaTime);

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
		

		// get back up after getting hit
		if(Time.time - prevSuspicionTime > getUpTime)
		{
			GetComponent<Transform>().eulerAngles = Vector3.zero;
		}
		if(hasWayPoints)
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
	}
}
