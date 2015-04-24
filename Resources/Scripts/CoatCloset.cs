using UnityEngine;
using System.Collections;

public class CoatCloset : MonoBehaviour {

	private bool pressedE, closedDistance, activated;
	private GameObject closetButton;


	void Start(){
		closetButton = GameObject.Find("closetButton");
		closedDistance = false;
		activated = false;
	}

	// enter the coat closet, time to reset
	void OnTriggerEnter(Collider collider)
	{
		if(collider.CompareTag("Bear"))
		{
			closedDistance = true;		
		}

		else{
			closedDistance = false;
		}
	}

	// reset the level
	private void Reset() 
	{
		string levelName = Application.loadedLevelName;
		//levelName = levelName + "_reloaded";
		Application.LoadLevel (levelName);
	}

	//activate UI if bear closes distance with closet
	void activateButton()
	{
		if (closedDistance && activated == false)
		{
			closetButton.active = true;
		}

		else
		{
			closetButton.active = false;
		}
	}

	void changeActivate()
	{
		activated = !activated;
	}

	void Update ()
	{
		activateButton();

		if (Input.GetKeyDown("e") && closedDistance)
		{
			activated = true;
			closetButton.active = false;
			GetComponent<AudioSource>().Play();
			// wait 2 seconds, to let changing audio play
			Invoke( "Reset", 2);	
			Invoke ("changeActivate", 2);
		}
	}
}