using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RegularNPC : MonoBehaviour {

	// the player
	private GameObject bear;
	private Bear bearScript;

	// the gender and dialogues
	public bool isFemale;
	private List<string> lowDialogues, midDialogues, highDialogues;
	private float preDialogueTime;
	private const float dialogueCooldown = 1f;

	// when guest is suspicious, and when to get back up after getting hit
	private float prevSuspicionTime;
	private const float suspicionCooldown = 0.5f;
	private const float getUpTime = 1.8f;
	private bool isUp;
	private Vector3 velocity;

	// actual NPC model
	private GameObject model;

	// for patrolling
	private float randomWalkCoodown;
	private float prevStartWalkTime;
	private bool isStopped;
	public float maxSpeed = 2f;
	private const float minStopTime = 3f;
	private const float maxStopTime = 6f;

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
		isUp = true;

		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();
		audios = GetComponents<AudioSource>();

		LoadDialogues();

		GetComponent<Rigidbody>().centerOfMass = new Vector3(0f,-1f,0f);
		model = GetComponent<Transform>().Find("model").gameObject;
	}

	// load file name's contents into the list
	private void LoadDialoguesFromFileToList(string name, List<string> list)
	{
		TextAsset txt;
		txt = Resources.Load(name) as TextAsset;
		string[] lines = txt.text.Split('\n');
		
		// read my text file
	  foreach (string line in lines)
	  {
			list.Add(line);
 		}
	}

	// load the dialogues from text, based on gender
	private void LoadDialogues()
	{
		lowDialogues = new List<string>();
		midDialogues = new List<string>();
		highDialogues = new List<string>();
		
		if(isFemale)
		{
			LoadDialoguesFromFileToList("Dialogues/femaleNPC_low", lowDialogues);
			LoadDialoguesFromFileToList("Dialogues/femaleNPC_mid", midDialogues);
			LoadDialoguesFromFileToList("Dialogues/femaleNPC_high", highDialogues);
		}
		else
		{
			LoadDialoguesFromFileToList("Dialogues/maleNPC_low", lowDialogues);
			LoadDialoguesFromFileToList("Dialogues/maleNPC_mid", midDialogues);
			LoadDialoguesFromFileToList("Dialogues/maleNPC_high", highDialogues);
		}
		
	}

	// bear runs into this NPC
	void OnCollisionEnter(Collision collision)
	{	
		// bear run into this NPC
		if(collision.gameObject.name.Equals("Bear"))
		{

			// prevent rapid increase by tiny small run-intos with guests
			if(Time.time - prevSuspicionTime > suspicionCooldown)
			{
				bearScript.IncreaseSuspicion(suspicionIncreaseUponCollision);
				
				if(bearScript.isDiscovered)
				{
					audios[1].Play();
				}
				else
				{
					audios[0].Play();
				}
				

				prevSuspicionTime = Time.time;
				isUp = false;

				model.GetComponent<Animator>().SetInteger("walkState",2);
			}
			
		}
	}


	// player near vincinity to talk
	void OnTriggerStay(Collider collider)
  {
    if(collider.CompareTag("Bear") && 
    	 !bearScript.isDiscovered && 
    	 Time.time - preDialogueTime > dialogueCooldown &&
    	 Input.GetKeyDown("e"))
    {
    	if(bearScript.suspicionPercent < 40f)
    	{
    		print(lowDialogues[Random.Range(0, lowDialogues.Count)]);
    	}
    	else if(bearScript.suspicionPercent < 80f)
    	{
    		print(midDialogues[Random.Range(0, midDialogues.Count)]);
    	}
    	else
    	{
    		print(highDialogues[Random.Range(0, highDialogues.Count)]);
    	}



    	preDialogueTime = Time.time;
    }
  }

	// random walk this NPC
	void Patrol()
	{
		// patrol cooldown
		if(Time.time - prevStartWalkTime > randomWalkCoodown)
		{
			// reset cooldown
			randomWalkCoodown = Random.Range(minStopTime, maxStopTime);
			prevStartWalkTime = Time.time;

			// if stopped, walk. If walking, stop
			if(isStopped)
			{
				velocity = Vector3.zero;
				Vector3 r = GetComponent<Transform>().eulerAngles;
				GetComponent<Transform>().localEulerAngles = new Vector3(0f, r.y, 0f);
				model.GetComponent<Animator>().SetInteger("walkState",0);
			}
			else
			{
				float x = Random.Range(-maxSpeed,maxSpeed);
				float z = Random.Range(-maxSpeed,maxSpeed);
				velocity = new Vector3(x,0f,z);
				float yRotation = Vector2.Angle(new Vector2(x, z), new Vector2(0f,1f));
        yRotation = x > 0f ? yRotation : -yRotation;
        GetComponent<Transform>().localEulerAngles = new Vector3(0f, yRotation, 0f);
        model.GetComponent<Animator>().SetInteger("walkState",1);
			}
			isStopped = !isStopped;
		}

		GetComponent<Rigidbody>().velocity = velocity;
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
	
	// Update is called once per frame
	void Update () 
	{	

		// only do things if NPC can do things
		if(Time.time - prevSuspicionTime > getUpTime)
		{
			// if down, get up
			if(!isUp)
			{
				GetComponent<Rigidbody>().angularVelocity = Vector3.zero;	
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				model.GetComponent<Animator>().SetInteger("walkState",0);
				isUp = true;
			}
			else
			{
				if(!bearScript.isDiscovered)	// random walk if there is no bear
				{
					if(maxSpeed > 0f)
					{
						Patrol();
					}
				}
				else					// run away if there is a bear
				{
					if(model.GetComponent<Animator>().GetInteger("walkState") != 2)
					{
						audios[1].Play();
					}

					RunAway();
				}
			}
		}

	}
}
