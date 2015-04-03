using UnityEngine;
using System.Collections;

public class Bear : MonoBehaviour {

	private bool isOnTwoLegs;

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


	// Use this for initialization
	void Start () 
	{
		isOnTwoLegs = false;
		yRotation = 0f;
		Vector3 p = GetComponent<Transform>().position;
		GetComponent<Transform>().eulerAngles = Vector3.up;
		GetComponent<Transform>().position = new Vector3(p.x,84.5f,p.z);
		audios = GetComponents<AudioSource>();
	}



	private void CheckLegsMode()
	{ 
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{

			audios[0].Play();

			isOnTwoLegs = !isOnTwoLegs;
    	GetComponent<Rigidbody>().velocity = Vector3.zero;
    	Vector3 p = GetComponent<Transform>().position;

			if(isOnTwoLegs)	// switch to 2 legs
			{
				GetComponent<Transform>().eulerAngles = Vector3.zero;
				GetComponent<Transform>().position = new Vector3(p.x,85f,p.z);
			}
			else						// switch to 4 legs
			{
				GetComponent<Transform>().eulerAngles = Vector3.up;
				GetComponent<Transform>().position = new Vector3(p.x,84.5f,p.z);
			}
		}
	}

	// get player keyboard input, do things
	private void CheckMovement()
	{
		Vector3 r = GetComponent<Transform>().eulerAngles;
		Vector3 v = GetComponent<Rigidbody>().velocity;
		if(isOnTwoLegs)		// two legs movement
		{
			if(Input.GetKey("w"))
    	{
      	if(v.x < max2LegWalkSpeed)	// limit walk speed
      	{
      		GetComponent<Rigidbody>().AddForce(new Vector3(default2LegForce, 0f, 0f));
      	}
      	if(r.z - deltaTilt > 360f - maxTilt || r.z <= 1f)	// limit tilt
      	{
      		GetComponent<Transform>().eulerAngles = new Vector3(r.x, r.y, r.z - deltaTilt);
      	}
    	}
    	else if(Input.GetKey("s"))
    	{
    		if(v.x > -max2LegWalkSpeed)	// limit walk speed
      	{
      		GetComponent<Rigidbody>().AddForce(new Vector3(-default2LegForce, 0f, 0f));
      	}
      	if(r.z + deltaTilt < maxTilt || r.z > 360f - maxTilt - 1f)	// limit tilt
      	{
      		GetComponent<Transform>().eulerAngles = new Vector3(r.x, r.y, r.z + deltaTilt);
      	}
    	}

    	if(Input.GetKey("a"))
    	{ 
    		if(v.z < max2LegWalkSpeed)	// limit walk speed
      	{
      		GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, default2LegForce));
      	}
      	if(r.x < maxTilt)						// limit tilt
      	{
      		GetComponent<Transform>().eulerAngles = new Vector3(r.x + deltaTilt, r.y, r.z);
      	}
    	}
    	else if(Input.GetKey("d"))
    	{ 
      	if(v.z > -max2LegWalkSpeed)	// limit walk speed
      	{
      		GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, -default2LegForce));
      	}
      	if(r.x > -maxTilt)					// limit tilt
      	{
      		GetComponent<Transform>().eulerAngles = new Vector3(r.x - deltaTilt, r.y, r.z);
      	}
    	}
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

    	GetComponent<Transform>().eulerAngles = new Vector3(0f,yRotation,90f);
		}
	}

	
	// Update is called once per frame
	void Update () 
	{
		CheckLegsMode();
		CheckMovement();
	}
}
