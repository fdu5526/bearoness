using UnityEngine;
using System.Collections;

public class Bear : MonoBehaviour {

	private bool isOnTwoLegs;

	private const float default4LegWalkSpeed = 3f;
	private const float default2LegForce = 0.6f;
	private const float deltaTilt = 0.001f;

	// Use this for initialization
	void Start () 
	{
		isOnTwoLegs = false;
	}



	private void CheckLegsMode()
	{ 
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			isOnTwoLegs = !isOnTwoLegs;
			if(isOnTwoLegs)
			{
				GetComponent<Transform>().localRotation = Quaternion.identity;
			}
			else
			{
				Quaternion i = Quaternion.identity;
			}
		}
	}

	// get player keyboard input, do things
	private void CheckMovement()
	{
		if(isOnTwoLegs)		// two legs movement
		{
			Quaternion r = GetComponent<Transform>().localRotation;
			if(Input.GetKey("w"))
    	{ 
      	GetComponent<Rigidbody>().AddForce(new Vector3(default2LegForce, 0f, 0f));
      	GetComponent<Transform>().localRotation = new Quaternion(r.x, r.y, r.z - deltaTilt, r.w);
    	}
    	else if(Input.GetKey("s"))
    	{
      	GetComponent<Rigidbody>().AddForce(new Vector3(-default2LegForce, 0f, 0f));
      	GetComponent<Transform>().localRotation = new Quaternion(r.x, r.y, r.z + deltaTilt, r.w);
    	}

    	if(Input.GetKey("a"))
    	{ 
      	GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, default2LegForce));
      	GetComponent<Transform>().localRotation = new Quaternion(r.x + deltaTilt, r.y, r.z, r.w);
    	}
    	else if(Input.GetKey("d"))
    	{ 
      	GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, -default2LegForce));
      	GetComponent<Transform>().localRotation = new Quaternion(r.x - deltaTilt, r.y, r.z, r.w);
    	}

    	// stop movement
    	if(!Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d"))
    	{
    		
    	}
		}
		else							// four legs movement
		{
			// up down movement
			Vector3 v = GetComponent<Rigidbody>().velocity;
			if(Input.GetKey("w"))
    	{ 
      	GetComponent<Rigidbody>().velocity = new Vector3(default4LegWalkSpeed, v.y, v.z);
    	}
    	else if(Input.GetKey("s"))
    	{
      	GetComponent<Rigidbody>().velocity = new Vector3(-default4LegWalkSpeed, v.y, v.z);
    	}

    	// left right movement
  		v = GetComponent<Rigidbody>().velocity;
    	if(Input.GetKey("a"))
    	{ 
      	GetComponent<Rigidbody>().velocity = new Vector3(v.x, v.y, default4LegWalkSpeed);
    	}
    	else if(Input.GetKey("d"))
    	{ 
      	GetComponent<Rigidbody>().velocity = new Vector3(v.x, v.y, -default4LegWalkSpeed);
    	}

    	// stop movement
    	if(!Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d"))
    	{
    		GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);	
    	}
		}
	}

	
	// Update is called once per frame
	void Update () 
	{
		CheckLegsMode();
		CheckMovement();
	}
}
