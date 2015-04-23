using UnityEngine;
using System.Collections;

public class SuspiciousArea : MonoBehaviour {

	// the bear script
	private Bear bearScript;

	// the amount to increase the suspicion when player is 4 legs
	private const float suspicionIncreaseCount = 0.2f;

	void Start()
	{
		GameObject bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();
	}


	// stay in the suspicion area, increase suspicion
	void OnTriggerStay(Collider collider)
	{
		if(collider.CompareTag("Bear"))
		{
			bearScript.IncreaseSuspicion(suspicionIncreaseCount);
		}
	}
}
