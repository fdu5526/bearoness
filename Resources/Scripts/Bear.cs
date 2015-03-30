using UnityEngine;
using System.Collections;

public class Bear : MonoBehaviour {


	public bool isOnTwoLegs;

	// Use this for initialization
	void Start () {
		isOnTwoLegs = false;	
	}



	private void CheckControl()
	{
		if(isOnTwoLegs)		// two legs movement
		{
			if(Input.GetKey("w"))
    	{ 
      
    	}
    	if(Input.GetKey("a"))
    	{ 
      
    	}
    	if(Input.GetKey("s"))
    	{ 
      
    	}
    	if(Input.GetKey("d"))
    	{ 
      
    	}
		}
		else							// four legs movement
		{

		}
	}

	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
