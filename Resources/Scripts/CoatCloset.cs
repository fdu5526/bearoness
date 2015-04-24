using UnityEngine;
using System.Collections;

public class CoatCloset : MonoBehaviour {

	private string reload = "_reload";
	public int checkpointNumber;

	// enter the coat closet, time to reset
	void OnTriggerEnter(Collider collider)
	{
		if(collider.CompareTag("Bear"))
		{
			GetComponent<AudioSource>().Play();

			// wait 2 seconds, to let changing audio play
			Invoke( "Reset", 2);			
		}
	}

	// reset the level
	private void Reset() 
	{
		string levelName = Application.loadedLevelName;
		if(levelName.Length < 8)
		{
			levelName = levelName + reload + checkpointNumber.ToString();
		}
		else
		{
			levelName = levelName.Substring(0, levelName.Length - 1) + checkpointNumber.ToString();
		}
		
		Application.LoadLevel (levelName);
	}
}
