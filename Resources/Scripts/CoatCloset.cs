using UnityEngine;
using System.Collections;

public class CoatCloset : MonoBehaviour {

	// enter the coat closet, time to reset
	void OnTriggerEnter(Collider collider)
	{
		if(collider.CompareTag("Bear"))
		{
			GetComponent<AudioSource>().Play();

			Invoke( "Reset", 2);

			
		}
	}


	private void Reset() 
	{
		string levelName = Application.loadedLevelName;
		//levelName = levelName + "_reloaded";
		Application.LoadLevel (levelName);
	}
}
