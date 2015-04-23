using UnityEngine;
using System.Collections;

public class GuardDetectionCircle : MonoBehaviour {

	// the player
	private GameObject bear;
	private Bear bearScript;

	// how large detection circle normally is, then how much it expands by
	private const float defaultDetectionDiameter = 5f;
	private const float extraDetectionDiameter = 15f;

	// how much suspicion to increase per frame when player in circle
	private const float twoLegSuspicionIncrease = 0.05f;
	private const float fourLegSuspicionIncrease = 5.0f;

	// Use this for initialization
	void Start () 
	{
		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();
	}

	void OnTriggerStay(Collider collider)
	{	
		// bear is within range
		if(collider.CompareTag("Bear"))
		{
			// guard suspicion for bear on 2 legs
			if(bearScript.isOnTwoLegs)
			{
				bearScript.IncreaseSuspicion(twoLegSuspicionIncrease);
			}
			// guard suspicion for bear on 4 legs
			else
			{
				bearScript.IncreaseSuspicion(fourLegSuspicionIncrease);
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		// expand based on player suspicion
		float d = bearScript.suspicionPercent / 100f * extraDetectionDiameter + defaultDetectionDiameter;
		GetComponent<Transform>().localScale = new Vector3(d, 0.15f, d);
	}
}
