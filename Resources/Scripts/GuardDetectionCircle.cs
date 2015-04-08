using UnityEngine;
using System.Collections;

public class GuardDetectionCircle : MonoBehaviour {

	// the player
	private GameObject bear;
	private Bear bearScript;

	// Use this for initialization
	void Start () 
	{
		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();
	}

	void OnTriggerStay(Collider collider)
	{	
		if(collider.CompareTag("Bear"))
		{
			if(bearScript.isOnTwoLegs)
			{
				bearScript.IncreaseSuspicion(0.05f);
			}
			else
			{
				bearScript.IncreaseSuspicion(0.5f);
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{

	}
}
