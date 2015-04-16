using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RegularNPC : MonoBehaviour {

	// the player
	private GameObject bear;
	private Bear bearScript;

	// the gender and dialogues
	private bool isFemale;
	private List<string> dialogues;

	// for patrolling
	private float randomWalkCoodown;
	private float prevStartWalkTime;
	private bool isStopped;
	private const float maxSpeed = 1.5f;
	private const float minStopTime = 0.9f;
	private const float maxStopTime = 3f;

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
		isFemale = Random.value >= 0.5f;
		prevStartWalkTime = 0f;

		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();
		audios = GetComponents<AudioSource>();

		LoadDialogues();
	}

	// load the dialogues from text, based on gender
	private void LoadDialogues()
	{
		dialogues = new List<string>();
		
		TextAsset txt;
		if(isFemale)
		{
			txt = Resources.Load("Dialogues/femaleNPCDialogue") as TextAsset;
		}
		else
		{
			txt = Resources.Load("Dialogues/maleNPCDialogue") as TextAsset;	
		}
		string[] lines = txt.text.Split('\n');
		
		// read my text file
	  foreach (string line in lines)
	  {
			dialogues.Add(line);
 		}
		
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


	// player near vincinity to talk
	void OnTriggerStay(Collider collider)
  {
    if(collider.CompareTag("Bear") && 
    	 !bearScript.isDiscovered && 
    	 Input.GetKeyDown("space"))
    {
    	print(dialogues[Random.Range(0, dialogues.Count)]);
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
				GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
			else
			{
				float x = Random.Range(-maxSpeed,maxSpeed);
				float z = Random.Range(-maxSpeed,maxSpeed);
				GetComponent<Rigidbody>().velocity = new Vector3(x,0f,z);
			}
			isStopped = !isStopped;
		}
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
		if(!bearScript.isDiscovered)	// random walk if there is no bear
		{
			Patrol();
		}
		else					// run away if there is a bear
		{
			RunAway();
		}
		

	}
}
