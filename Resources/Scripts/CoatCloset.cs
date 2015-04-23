using UnityEngine;
using System.Collections;

public class CoatCloset : MonoBehaviour {

	// enter the coat closet, time to reset
	void OnTriggerEnter(Collider collider)
	{
		if(collider.CompareTag("Bear"))
		{
			string levelName = Application.loadedLevelName;
			//levelName = levelName + "_reloaded";
			Application.LoadLevel (levelName);
		}
	}
}
