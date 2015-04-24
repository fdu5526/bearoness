using UnityEngine;
using System.Collections;

public class CoatCloset : MonoBehaviour {

	private bool pressedE, closedDistance;
	private GameObject closetButton;


	void Start()
	{
		closetButton = GameObject.Find("closeButton");
	}
	
	// enter the coat closet, time to reset
	void OnTriggerEnter(Collider collider)
	{
		if(collider.CompareTag("Bear"))
		{
			closedDistance = true;		
		}
	}

	// reset the level
	private void Reset() 
	{
		string levelName = Application.loadedLevelName;
		//levelName = levelName + "_reloaded";
		Application.LoadLevel (levelName);
	}

	void activateButton()
	{
		if (closedDistance)
		{
			closetButton.active = true;
		}

		else
		{
			closetButton.active = false;
		}
	}

	void Update ()
	{
		//activateButton();

		if (pressedE)
		{
			GetComponent<AudioSource>().Play();
			// wait 2 seconds, to let changing audio play
			Invoke( "Reset", 2);	
		}
	}
}
