using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Bear : MonoBehaviour 
{

////////////////////////////////////// public vars & functions /////////////////////////////

  // how suspicious player is being
  public float suspicionPercent;

  // whether bear is walking on 2 legs
  public bool isOnTwoLegs;

  // whether player can move
  public bool isDisabled;

  // player does suspicious things, gain suspicion
  public void IncreaseSuspicion(float amount)
  {
    lastSuspicionTime = Time.time;
    //TODOsuspicionPercent = Math.Min(100f, suspicionPercent + amount);
  }

  // if player is completely discovered, no going back to pretending
  public bool isDiscovered { get { return suspicionPercent >= 99.9f; } }

////////////////////////////////////// private vars ////////////////////////////////////////	

  // 5 seconds since last discovery before player start to lose suspicion
  // amount of suspicion the player loses per frame
  // the last time player was suspicious
  private const float suspicionCooldown = 5f;
  private const float deltaSuspicion = 0.05f;
  private float lastSuspicionTime;

  // the actual bear model, and suspicion meter
  private GameObject bearModel;
  private Slider suspicionMeter;

  private bool isWalking;

  // speed of 4 legs mode, and which direction player is facing
  private const float default4LegWalkSpeed = 15f;
  private const float sqrtRoot2 = 1.414f;
  private float yRotation;
  
  // speed and force for 2 leg mode
  private const float default2LegForce = 20f;
  private const float max2LegWalkSpeed = 7f;
    
  // how much player tilt during 2 leg mode
  private const float deltaTilt = 1f;
  private const float maxTilt = 30f;

  // audios
  private AudioSource[] audios;
  private AudioSource growl, run2, run4;

///////////////////////////////////////////////////////////////////////////////////////////  

  // Use this for initialization
	void Start () 
	{
		suspicionPercent = 0f;
		yRotation = 0f;
    lastSuspicionTime = 0f;

		audios = GetComponents<AudioSource>();
    growl = audios[0];
    run2 = audios[1];
    run4 = audios[2];

    bearModel = GetComponent<Transform>().Find("BearModel").gameObject;
    suspicionMeter = GameObject.Find("UI").GetComponent<Transform>().Find("SuspicionMeter").gameObject.GetComponent<Slider>();
    SwitchLegMode();

	}



  void OnTriggerEnter(Collider collider)
  {
    print(collider.tag);
  }

  // swap to current leg mode
  private void SwitchLegMode()
  {
    bearModel.GetComponent<Animator>().SetBool("isOnTwoLegs", isOnTwoLegs);
    GetComponent<Collider>().enabled = !isOnTwoLegs;
    bearModel.GetComponent<Collider>().enabled = isOnTwoLegs;

    if(!isOnTwoLegs)
    {
      bearModel.GetComponent<Transform>().localEulerAngles = new Vector3(270f, 0f, 0f);
    }

    GetComponent<Rigidbody>().velocity = Vector3.zero;
  }

  // if player changes movement mode, make appropriate adjustments
	private void CheckLegsMode()
	{ 
		if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			growl.Play();

			isOnTwoLegs = !isOnTwoLegs;
      SwitchLegMode();
		}
	}

  // swap out bear clothing to something random
  private void ChangeToRandomClothing()
  {
    GameObject polysurface = bearModel.GetComponent<Transform>().Find("polySurface1").gameObject;

    int ri1 = UnityEngine.Random.Range(0, 9);
    int ri2 = UnityEngine.Random.Range(0, 9);
    while(ri2 == ri1)
    {
      ri2 = UnityEngine.Random.Range(0, 9);
    }


    Material m1 = Resources.Load("ClothesMaterials/" + ri1, typeof(Material)) as Material;
    Material m2 = Resources.Load("ClothesMaterials/" + ri2, typeof(Material)) as Material;
    
    Renderer r = polysurface.GetComponent<Renderer>();
    Material[] materials = r.materials;
    
    materials[1] = m1;
    materials[2] = m2;

    r.materials = materials;
    
  }


	// get player keyboard input, do things
	private void CheckMovement()
	{
		Vector3 v = GetComponent<Rigidbody>().velocity;
    isWalking = false;

		if(isOnTwoLegs)		// two legs movement
		{
			Vector3 r = bearModel.GetComponent<Transform>().eulerAngles;
      float xRotation = r.x;
      float zRotation = r.z;

			if(Input.GetKey("w"))
    	{
      	if(v.z < max2LegWalkSpeed)	// limit walk speed
      	{
      		GetComponent<Rigidbody>().AddForce(new Vector3(default2LegForce, 0f, default2LegForce));
      	}
      	if(xRotation - deltaTilt > 360f - maxTilt || xRotation < 360f - maxTilt - deltaTilt) // limit tilt
        {
          xRotation -= deltaTilt;
        }
        isWalking = true;
    	}
    	else if(Input.GetKey("s"))
    	{
    		if(v.z > -max2LegWalkSpeed)	// limit walk speed
      	{
      		GetComponent<Rigidbody>().AddForce(new Vector3(-default2LegForce, 0f, -default2LegForce));
      	}
      	if(xRotation + deltaTilt < maxTilt || xRotation > maxTilt + deltaTilt) // limit tilt
        {
          xRotation += deltaTilt;
        }
        isWalking = true;
    	}

    	if(Input.GetKey("a"))
    	{ 
    		if(v.x > -max2LegWalkSpeed)	// limit walk speed
      	{
      		GetComponent<Rigidbody>().AddForce(new Vector3(-default2LegForce, 0f, default2LegForce));
      	}
        if(zRotation + deltaTilt < maxTilt || zRotation > maxTilt + deltaTilt)  // limit tilt
        {
          zRotation += deltaTilt;
        }
        isWalking = true;
      	
    	}
    	else if(Input.GetKey("d"))
    	{ 
      	if(v.x < max2LegWalkSpeed)	// limit walk speed
      	{
      		GetComponent<Rigidbody>().AddForce(new Vector3(default2LegForce, 0f, -default2LegForce));
      	}
        if(zRotation - deltaTilt > 360f - maxTilt || zRotation < 360f - maxTilt - deltaTilt)  // limit tilt
        {
          zRotation -= deltaTilt;
        }
      	isWalking = true;
    	}

      // get which direction player is facing
      v = GetComponent<Rigidbody>().velocity;

      if(v.magnitude > 1f)  // prevent crazy bounce due to small velocity changes
      {
        float tempy = Vector2.Angle(new Vector2(v.x, v.z), new Vector2(0f,1f));
        tempy = v.x > 0f ? tempy : -tempy;

        // black magic hax to fix immediate 180 degree flip
        float dy = Math.Abs(tempy - yRotation);
        if(dy > 140f && dy < 300f)
        {
          yRotation = (yRotation + tempy) / 2;
        }
        else
        {
          yRotation = tempy;
        }
      }

      // perform actual rotation
      bearModel.GetComponent<Transform>().eulerAngles = new Vector3(xRotation, yRotation, zRotation);
		}
		else							// four legs movement
		{
		 
      if(Input.GetKey("w"))
      {
        if(Input.GetKey("a"))
        {
          GetComponent<Rigidbody>().velocity = new Vector3(0f, v.y, default4LegWalkSpeed);
          yRotation = 0f;
        }
        else if(Input.GetKey("d"))
        {
          GetComponent<Rigidbody>().velocity = new Vector3(default4LegWalkSpeed, v.y, 0f);
          yRotation = 90f;
        }
        else
        {
          GetComponent<Rigidbody>().velocity = new Vector3(default4LegWalkSpeed/sqrtRoot2, v.y, default4LegWalkSpeed/sqrtRoot2);
          yRotation = 45f;
        }
      }
      else if(Input.GetKey("s"))
      {
        if(Input.GetKey("a"))
        {
          GetComponent<Rigidbody>().velocity = new Vector3(-default4LegWalkSpeed, v.y, 0f);
          yRotation = 270f;
        }
        else if(Input.GetKey("d"))
        {
          GetComponent<Rigidbody>().velocity = new Vector3(0f, v.y, -default4LegWalkSpeed);
          yRotation = 180f;
        }
        else
        {
          GetComponent<Rigidbody>().velocity = new Vector3(-default4LegWalkSpeed/sqrtRoot2, v.y, -default4LegWalkSpeed/sqrtRoot2);
          yRotation = 225f;
        }
      }
      else if(Input.GetKey("a"))
      {
        GetComponent<Rigidbody>().velocity = new Vector3(-default4LegWalkSpeed/sqrtRoot2, v.y, default4LegWalkSpeed/sqrtRoot2);
        yRotation = 315f;
      }
      else if(Input.GetKey("d"))
      {
        GetComponent<Rigidbody>().velocity = new Vector3(default4LegWalkSpeed/sqrtRoot2, v.y, -default4LegWalkSpeed/sqrtRoot2);
        yRotation = 135f;
      }
      else
      {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
      }

      // if we pressed WASD, then we are walkin
      isWalking = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");

    	// rotate bear towards correct direction
    	GetComponent<Transform>().eulerAngles = new Vector3(90f,yRotation,0f);
		}
	}

    // play stepping sounds when moving
    void StepSounds()
    {
      if (isOnTwoLegs && isWalking)
      {
        /*
        if(!run2.loop)
        {
          run2.loop = true;
          run2.Play();
          run4.loop = false;
        }
        */
        run4.loop = false;
        
      }
      if (!isOnTwoLegs && isWalking)
      {
        
        if(!run4.loop)
        {
          run4.loop = true;
          run4.Play();
          //run2.loop = false;
        }
      }
      if(!isWalking)
      {
        //run2.loop = false;
        run4.loop = false;
      }
    }


	



	// Update is called once per frame
	void Update () 
	{
    //TODO
    if(Input.GetKeyDown("e"))
    {
      ChangeToRandomClothing();
    }

    suspicionMeter.value = suspicionPercent;

    if(!isDisabled)
		{
      CheckLegsMode();
  		CheckMovement();
      StepSounds();

      bearModel.GetComponent<Animator>().SetBool("isFourlegsRunning", isWalking && !isOnTwoLegs);

      // lose suspicion if doing nothing suspicious for a while
      if(!isDiscovered && Time.time - lastSuspicionTime > suspicionCooldown)
      {
        suspicionPercent = Math.Max(0f, suspicionPercent - deltaSuspicion);
      }
    }
	}
}
