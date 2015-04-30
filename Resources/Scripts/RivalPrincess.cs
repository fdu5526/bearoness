using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RivalPrincess : MonoBehaviour {

	// the player
	private GameObject bear;
	private Bear bearScript;

	// when guest is suspicious, and when to get back up after getting hit
	private float prevSuspicionTime;
	private const float suspicionCooldown = 0.5f;
	private const float getUpTime = 1.8f;
	private bool isUp;
	private Vector3 velocity;
	private bool isPoisoned;

	// actual NPC model
	private GameObject model;

	// for when player is too suspicious
	private const float runAwaySpeed = 20f;

	// how much a collision should increase suspicion by
	private const float suspicionIncreaseUponCollision = 10f;

	// audio
	private AudioSource[] audios;

	// Use this for initialization
	void Start ()
	{
		isUp = true;
		isPoisoned = false;

		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();
		audios = GetComponents<AudioSource>();

		GetComponent<Rigidbody>().centerOfMass = new Vector3(0f,-1f,0f);
		model = GetComponent<Transform>().Find("model").gameObject;

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
    	 !bearScript.isDisabled &&
    	 bearScript.hasDrinkPlatter &&
    	 Input.GetKeyDown("e"))
    {
    	bearScript.isDisabled = true;
    	bearScript.RemoveDrinkPlatter();

    	audios[3].Play();

    	Invoke( "GetPoisoned", 6);
    	Invoke( "Level2Victory", 10);

    }
  }

  // get poisoned, run away really fast, ignore gravity and collision
  void GetPoisoned()
  {
  	GetComponent<Rigidbody>().useGravity = false;
  	GetComponent<Rigidbody>().drag = 0f;
  	GetComponent<Collider>().enabled = false;
  	isPoisoned = true;
  	GetComponent<Transform>().eulerAngles = new Vector3(0f,180f,0f);
  	model.GetComponent<Animator>().SetInteger("walkState",2);
  	audios[2].Play();
  }

  // move onto level 3
  void Level2Victory()
  {
  	GameObject v = GameObject.Find("UI").GetComponent<Transform>().Find("Victory pop-up").gameObject;
  	v.SetActive(true);
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

		if(isPoisoned)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,-30f);
		}
		else
		{
			// only do things if NPC can do things
			if(Time.time - prevSuspicionTime > getUpTime)
			{
				// if down, get up
				if(!isUp)
				{
					GetComponent<Rigidbody>().angularVelocity = Vector3.zero;	
					GetComponent<Transform>().eulerAngles = Vector3.zero;
					GetComponent<Rigidbody>().velocity = Vector3.zero;
					model.GetComponent<Animator>().SetInteger("walkState",0);
					isUp = true;
				}
				else
				{
					if(bearScript.isDiscovered)	// run if there is bear
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
}
