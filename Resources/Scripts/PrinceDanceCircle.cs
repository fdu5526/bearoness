using UnityEngine;
using System.Collections;

public class PrinceDanceCircle : MonoBehaviour {

	// the player
	private GameObject bear;
	private Bear bearScript;

	// how large detection circle normally is, then how much it expands by
	private float defaultAllowanceDiameter = 5f;
	private const float upAllowanceDiameter = 0.2f;
	private const float lowerAllowanceDiameter = 0.5f;

	// how much suspicion to increase per frame when player in circle
	private const float twoLegSuspicionIncrease = 0.05f;
	private const float fourLegSuspicionIncrease = 5.0f;

	//check if player has entered radius
	private bool hasEntered;

	// Use this for initialization
	void Start () 
	{
		hasEntered = false;
		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();
	}

	void increaseAllowance(){
		defaultAllowanceDiameter += upAllowanceDiameter;
	}

	void decreaseAllowance(){
		defaultAllowanceDiameter -= lowerAllowanceDiameter;
	}

	void OnTriggerStay(Collider collider)
	{	
		// bear is within range
		if(collider.CompareTag("Bear"))
		{
			hasEntered = true;
			// increase dance radius allowance for time in circle
			if(bearScript.isOnTwoLegs && defaultAllowanceDiameter <= 10f)
			{
				increaseAllowance();
			}
		//bear leaves radius
		else{
			if (hasEntered == true && bearScript.isOnTwoLegs){}
				if (defaultAllowanceDiameter >= 3f){
					decreaseAllowance();
				}		
			}
		}
	}


	// Update is called once per frame
	void Update () 
	{
		// expand based on player suspicion
		float d = defaultAllowanceDiameter;
		GetComponent<Transform>().localScale = new Vector3(d, 0.15f, d);
	}
}
