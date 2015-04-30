using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoatCloset1: MonoBehaviour {

	private bool pressedE, closedDistance, activated;
	private GameObject closetButton;
	//private Image fadeBlack;
	private Color c;

	private GameObject bear;
	private Bear bearScript;


	void Start(){
		bear = GameObject.Find("Bear");
		bearScript = bear.GetComponent<Bear>();
		closetButton = GameObject.Find("closetE 1");
		//fadeBlack = GameObject.Find("FadeBlackObj").GetComponent<Image>();
		closetButton.SetActive(false);
		closedDistance = false;
		activated = false;
		//c = fadeBlack.color;
		//c.a = 0;
		//fadeBlack.color = c;
	}

	// enter the coat closet, time to reset
	void checkDistance()
	{
		if (Vector3.Distance(transform.position, bear.transform.position) < 5f)
		{
			Debug.Log("in range");
			closedDistance = true;
		}

		else{
			closedDistance = false;
		}
	}

	/*void fadeToBlack()
	{
		c = fadeBlack.color; 
		while (c.a < 100f){
			c.a += 10f;
			fadeBlack.color = c;
		}
	}*/

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
			closetButton.SetActive(true);
			activated = true;
		}

		else if (!closedDistance && activated == true)
		{
			closetButton.SetActive(false);
			activated = false;
		}
	}

	void changeActivate()
	{
		activated = !activated;
	}

	void Update ()
	{
		activateButton();
		checkDistance();

		if (Input.GetKeyDown("e") && closedDistance)
		{
			closetButton.SetActive(false);
			GetComponent<AudioSource>().Play();
			//fadeToBlack();
			// wait 2 seconds, to let changing audio play
			Invoke( "Reset", 2);	
			Invoke ("changeActivate", 2);
		}
	}
}
