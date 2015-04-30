using UnityEngine;
using UnityEngine.UI;
using System;
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
	
	// actual NPC model
	private GameObject model;

	//waypoints
	public GameObject[] waypoints;
	private GameObject currentWaypoint;
	private int currentIndex;
	private bool hasWayPoints;

	// the dance circle
	private GameObject danceCircle;
	private GameObject danceMeter;
	private Slider danceMeterSlider;
	private bool danceCirclePresent;
	public float danceValue;

	//dance speed
	public int moveSpeed;
	private float minDistance = 0.2f;
	public bool distanceClosed;
	public bool pressedE;

	//UI junk
	private GameObject button, danceTutWindow;
	private bool activeDanceTut;


	// for when player is too suspicious
	private const float runAwaySpeed = 20f;

	// how much a collision should increase suspicion by
	private const float suspicionIncreaseUponCollision = 10f;

	// audio
	private AudioSource[] audios;

	// Use this for initialization
	void Start () 
	{
		danceCircle = Instantiate ((GameObject)(Resources.Load("Prefabs/PrinceDanceCircle")));
		danceCircle.SetActive(false);

		danceValue = 0f;
		danceMeterSlider = GameObject.Find("UI").GetComponent<Transform>().Find("DanceMeter").gameObject.GetComponent<Slider>();
		danceMeter = GameObject.Find("DanceMeter");
		danceMeter.SetActive(false);

		danceTutWindow = GameObject.Find("DanceTutWindow");
		danceTutWindow.SetActive(false);
		activeDanceTut = false;

		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();

		button = GameObject.Find("e key");
		audios = GetComponents<AudioSource>();

		model = GetComponent<Transform>().Find("model").gameObject;

		pressedE = false;
		danceCirclePresent = false;
		prevStartWalkTime = 0f;

		// if this has waypoints, get waypoints
		hasWayPoints = (waypoints != null && waypoints.Length > 0);
		if(hasWayPoints)
		{
			currentWaypoint = waypoints[0];
			currentIndex = 0;
		}
	}

	// bear runs into this NPC
	void OnCollisionEnter(Collision collision)
	{	
		// bear run into this NPC
		if(collision.gameObject.name.Equals("Bear"))
		{
			if (danceValue > 0f)
				{
					danceValue -= 0.04f;
				}
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


	// there is a bear in the room omg run away ahhhhhhhhhhhhhhhhh
	void RunAway()
	{
		model.GetComponent<Animator>().SetInteger("walkState",2);
		Vector3 tp = GetComponent<Transform>().position;
		Vector3 bp = bear.GetComponent<Transform>().position;
		
		// set direction and magnitude
		Vector3 d = tp - bp;
		d.Normalize();
		d *= runAwaySpeed;

		// gotta run
		GetComponent<Rigidbody>().velocity = d;
	}

	void checkBearDistance()
	{
		if (Vector3.Distance(bear.transform.position, transform.position) < 10f)
		{
			distanceClosed = true;
			if (pressedE == false)
			{
				button.SetActive(true);
			}
			else
			{
				button.SetActive(false);
			}
		}
		else
		{
			button.SetActive(false);
			distanceClosed = false;
		}
	}

	// put the dance circle where the guard is
	private void SetDanceCirclePosition()
	{
		Vector3 v = GetComponent<Transform>().position;
		float y = GetComponent<Transform>().position.y - 0.95f;
		danceCircle.GetComponent<Transform>().position = new Vector3(v.x, y, v.z);
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(!pressedE)
		{
			checkBearDistance();
		}

		if (activeDanceTut == false)
		{
			danceTutWindow.SetActive(false);
		}

		if (danceCirclePresent)
		{
			SetDanceCirclePosition();
		}

		if (Input.GetKeyDown(KeyCode.E) && distanceClosed){
			pressedE = true;
			activeDanceTut = true;
			Time.timeScale = 0;
		}

		if (danceValue >= 100f)
		{
			
			//you fuckin did it
			Application.LoadLevel ("endingAnimation");
		}


		if(!bearScript.isDiscovered && pressedE )	// drop dance circle and get down my dude
		{
			danceMeterSlider.value = danceValue;

			if (activeDanceTut)
			{
				danceTutWindow.SetActive(true);
				if (Input.GetKeyDown("space"))
					{
						danceTutWindow.SetActive(false);
						Time.timeScale = 1;
						activeDanceTut = false;

						AudioSource[] musics = GameObject.Find("Main Camera").GetComponents<AudioSource>();

						musics[0].Stop();
						musics[2].Play();

						model.GetComponent<Animator>().SetInteger("walkState",1);
						
						danceMeter.SetActive(true);
						danceCirclePresent = true;
						danceCircle.SetActive(true);
					}
			}
			MoveTowardWaypoint();

			if (Vector3.Distance (currentWaypoint.transform.position, transform.position) < minDistance) {
				currentIndex += 1;
				if (currentIndex > waypoints.Length - 1){
					currentIndex = 0;
				}
				currentWaypoint = waypoints [currentIndex];
			}

			if (danceCirclePresent && danceValue > 0f)
			{
					danceValue -= 0.04f;
			}
		}
		else if (bearScript.isDiscovered)				// run away if there is a bear
		{
			RunAway();
		}
		

	}
}
