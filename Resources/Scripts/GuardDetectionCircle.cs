using UnityEngine;
using System.Collections;

public class GuardDetectionCircle : MonoBehaviour {

	// the player
	private GameObject bear;
	private Bear bearScript;

	// how large detection circle normally is, then how much it expands by
	private const float defaultDetectionDiameter = 3f;
	private const float extraDetectionDiameter = 7f;

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
				bearScript.IncreaseSuspicion(0.05f);
			}
			// guard suspicion for bear on 4 legs
			else
			{
				bearScript.IncreaseSuspicion(0.5f);
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
