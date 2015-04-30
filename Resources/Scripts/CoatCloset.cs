using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoatCloset : MonoBehaviour {

	private bool pressedE, closedDistance;
	private GameObject closetButton;
	private GameObject bear;
	private Bear bearScript;

	private string reload = "_reload";
	public int checkpointNumber;


	void Start(){
		bear = GameObject.Find("Bear");
		bearScript = bear.GetComponent<Bear>();
		closetButton = GameObject.Find("UI").GetComponent<Transform>().GetChild(6).gameObject;

		closedDistance = false;
	}

	// enter the coat closet, time to reset
	void OnTriggerEnter(Collider collider)
	{
		bool b = collider.CompareTag("Bear");
		closetButton.SetActive(b);
		closedDistance = b;
	}


	void OnTriggerExit(Collider collider)
	{
		bool b = collider.CompareTag("Bear");
		closetButton.SetActive(b);
		closedDistance = b;
	}


	void OnTriggerStay(Collider collider)
	{
		if (Input.GetKeyDown("e") && closedDistance)
		{
			closetButton.SetActive(false);
			GetComponent<AudioSource>().Play();
			bearScript.isDisabled = true;

			// wait 2 seconds, to let changing audio play
			Invoke("Reset", 4);
		}
	}

	// reset the level
	private void Reset() 
	{
		string levelName = Application.loadedLevelName;
		/*if(levelName.Length < 8)
		{
			levelName = levelName + reload + checkpointNumber.ToString();
		}
		else
		{
			levelName = levelName.Substring(0, levelName.Length - 1) + checkpointNumber.ToString();
		}*/
		
		Application.LoadLevel (levelName);
	}


	void Update ()
	{
		//activateButton();
		//checkDistance();
	}
}