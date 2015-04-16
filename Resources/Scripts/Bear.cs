using UnityEngine;
using System;
using System.Collections;

public class Bear : MonoBehaviour 
{

////////////////////////////////////// public vars & functions /////////////////////////////

  // how suspicious player is being
  public float suspicionPercent;

  // whether bear is walking on 2 legs
  public bool isOnTwoLegs;

  // player does suspicious things, gain suspicion
  public void IncreaseSuspicion(float amount)
  {
    lastSuspicionTime = Time.time;
    suspicionPercent = Math.Min(100f, suspicionPercent + amount);
  }

  // if player is completely discovered, no going back to pretending
  public bool isDiscovered { get { return suspicionPercent >= 99.9f; } }

////////////////////////////////////// private vars ////////////////////////////////////////	

  // 5 seconds since last discovery before player start to lose suspicion
  // amount of suspicion the player loses per frame
  // the last time player was suspicious
  private const float suspicionCooldown = 5f;
  private const float deltaSuspicion = 0.15f;
  private float lastSuspicionTime;

  // speed of 4 legs mode, and which direction player is facing
  private const float default4LegWalkSpeed = 10f;
  private float yRotation;
  
  // speed and force for 2 leg mode
  private const float default2LegForce = 2f;
  private const float max2LegWalkSpeed = 4f;
    
  // how much player tilt during 2 leg mode
  private const float deltaTilt = 1f;
  private const float maxTilt = 40f;

  // audios
  private AudioSource[] audios;
  private AudioSource growl, run2, run4;

///////////////////////////////////////////////////////////////////////////////////////////  

  // Use this for initialization
	void Start () 
	{
		suspicionPercent = 0f;
    isOnTwoLegs = false;
		yRotation = 0f;
    lastSuspicionTime = 0f;

		Vector3 p = GetComponent<Transform>().position;
		GetComponent<Transform>().eulerAngles = new Vector3(0f,-90f,0f);
		GetComponent<Transform>().position = new Vector3(p.x,84.5f,p.z);
		audios = GetComponents<AudioSource>();
    growl = audios[0];
    run2 = audios[1];
    run4 = audios[2];


		//TODO hack for demo, do not keep forever
		Transform t = GetComponent<Transform>();
		t.Find("bear2LegPlaceholder").gameObject.GetComponent<MeshRenderer>().enabled = false;
		t.Find("bear4LegPlaceholder").gameObject.GetComponent<MeshRenderer>().enabled = true;
	}


  // if player changes movement mode, make appropriate adjustments
	private void CheckLegsMode()
	{ 
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			growl.Play();

			isOnTwoLegs = !isOnTwoLegs;
    	GetComponent<Rigidbody>().velocity = Vector3.zero;
    	Vector3 p = GetComponent<Transform>().position;

			if(isOnTwoLegs)	// switch to 2 legs
			{
				GetComponent<Transform>().eulerAngles = Vector3.zero;
				GetComponent<Transform>().position = new Vector3(p.x,84.8f,p.z);

				//TODO hack for demo, do not keep forever
				Transform t = GetComponent<Transform>();
				t.Find("bear2LegPlaceholder").gameObject.GetComponent<MeshRenderer>().enabled = true;
				t.Find("bear4LegPlaceholder").gameObject.GetComponent<MeshRenderer>().enabled = false;
			}
			else						// switch to 4 legs
			{
				GetComponent<Transform>().eulerAngles = Vector3.zero;
				GetComponent<Transform>().position = new Vector3(p.x,83.73f,p.z);

				//TODO hack for demo, do not keep forever
				Transform t = GetComponent<Transform>();
				t.Find("bear2LegPlaceholder").gameObject.GetComponent<MeshRenderer>().enabled = false;
				t.Find("bear4LegPlaceholder").gameObject.GetComponent<MeshRenderer>().enabled = true;
			}
		}
	}

	// get player keyboard input, do things
	private void CheckMovement()
	{
		Vector3 v = GetComponent<Rigidbody>().velocity;

		if(isOnTwoLegs)		// two legs movement
		{
			Vector3 r = GetComponent<Transform>().eulerAngles;
      float xRotation = r.x;
      float zRotation = r.z;

			if(Input.GetKey("w"))
    	{
      	if(v.x < max2LegWalkSpeed)	// limit walk speed
      	{
      		GetComponent<Rigidbody>().AddForce(new Vector3(default2LegForce, 0f, 0f));
      	}
      	if(zRotation - deltaTilt > 360f - maxTilt || zRotation < 360f - maxTilt - deltaTilt)	// limit tilt
      	{
          zRotation -= deltaTilt;
      	}
    	}
    	else if(Input.GetKey("s"))
    	{
    		if(v.x > -max2LegWalkSpeed)	// limit walk speed
      	{
      		GetComponent<Rigidbody>().AddForce(new Vector3(-default2LegForce, 0f, 0f));
      	}
      	if(zRotation + deltaTilt < maxTilt || zRotation > maxTilt + deltaTilt)	// limit tilt
      	{
          zRotation += deltaTilt;
      	}
    	}

    	if(Input.GetKey("a"))
    	{ 
    		if(v.z < max2LegWalkSpeed)	// limit walk speed
      	{
      		GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, default2LegForce));
      	}
      	if(xRotation + deltaTilt < maxTilt || xRotation > maxTilt + deltaTilt)	// limit tilt
      	{
          xRotation += deltaTilt;
      		
      	}
    	}
    	else if(Input.GetKey("d"))
    	{ 
      	if(v.z > -max2LegWalkSpeed)	// limit walk speed
      	{
      		GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, -default2LegForce));
      	}
      	if(xRotation - deltaTilt > 360f - maxTilt || xRotation < 360f - maxTilt - deltaTilt)	// limit tilt
      	{
          xRotation -= deltaTilt;
      	}
    	}

      // get which direction player is facing
      v = GetComponent<Rigidbody>().velocity;
      float yRotation = Vector2.Angle(new Vector2(v.x, v.z), new Vector2(1f,0f));
      yRotation = v.z > 0f ? -yRotation : yRotation;

      // perform actual rotation
      GetComponent<Transform>().eulerAngles = new Vector3(xRotation, yRotation, zRotation);
		}
		else							// four legs movement
		{
			bool isMovingForwardOrBackward = true;

			// up down movement
			if(Input.GetKey("w"))
    	{
      	GetComponent<Rigidbody>().velocity = new Vector3(default4LegWalkSpeed, v.y, v.z);
      	yRotation = 0f;
    	}
    	else if(Input.GetKey("s"))
    	{
      	GetComponent<Rigidbody>().velocity = new Vector3(-default4LegWalkSpeed, v.y, v.z);
      	yRotation = 180f;
    	}
    	else
    	{
    		GetComponent<Rigidbody>().velocity = new Vector3(0f, v.y, v.z);
    		isMovingForwardOrBackward = false;
    	}

    	// left right movement
  		v = GetComponent<Rigidbody>().velocity;
    	if(Input.GetKey("a"))
    	{ 
      	GetComponent<Rigidbody>().velocity = new Vector3(v.x, v.y, default4LegWalkSpeed);
      	if(isMovingForwardOrBackward)
      	{
      		if(yRotation == 0f)
      		{
      			yRotation = 315f;
      		}
      		else
      		{
      			yRotation = 225f;
      		}
      	}
      	else
      	{
      		yRotation = 270f;
      	}
    	}
    	else if(Input.GetKey("d"))
    	{ 
      	GetComponent<Rigidbody>().velocity = new Vector3(v.x, v.y, -default4LegWalkSpeed);
      	if(isMovingForwardOrBackward)
      	{
      		if(yRotation == 0f)
      		{
      			yRotation = 45f;
      		}
      		else
      		{
      			yRotation = 135f;
      		}
      	}
      	else
      	{
      		yRotation = 90f;
      	}
    	}
    	else
    	{ 
      	GetComponent<Rigidbody>().velocity = new Vector3(v.x, v.y, 0f);
    	}

    	// rotate bear towards correct direction
      // TODO fix this when real bear model added
    	GetComponent<Transform>().eulerAngles = new Vector3(0f,yRotation,90f);
		}
	}

    void stepCheck(){
      /*if (isOnTwoLegs && (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d"))){

        run2.loop = true;
        run2.Play();
      }*/
      if (!isOnTwoLegs && (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d"))){
        run4.loop = true;
        run4.Play();
      }
      else if (isOnTwoLegs || (Input.GetKey("w") == false && Input.GetKey("a") == false && Input.GetKey("s") == false && Input.GetKey("d") == false)){
        run4.loop = false;

      }
    }


	



	// Update is called once per frame
	void Update () 
	{
		CheckLegsMode();
		CheckMovement();
    stepCheck();

    // lose suspicion if doing nothing suspicious for a while
    if(!isDiscovered && Time.time - lastSuspicionTime > suspicionCooldown)
    {
      suspicionPercent = Math.Max(0f, suspicionPercent - deltaSuspicion);
    }
	}
}
