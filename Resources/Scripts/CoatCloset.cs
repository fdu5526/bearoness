using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoatCloset : MonoBehaviour {

	private bool pressedE, closedDistance, activated;
	private GameObject closetButton;
	//private Image fadeBlack;
	private Color c;


	void Start(){
		closetButton = GameObject.Find("closetE");
		//fadeBlack = GameObject.Find("FadeBlackObj").GetComponent<Image>();
		closedDistance = false;
		activated = false;
		//c = fadeBlack.color;
		//c.a = 0;
		//fadeBlack.color = c;
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
		}

		else
		{
			closetButton.SetActive(false);
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
			closetButton.SetActive(false);
			GetComponent<AudioSource>().Play();
			//fadeToBlack();
			// wait 2 seconds, to let changing audio play
			Invoke( "Reset", 2);	
			Invoke ("changeActivate", 2);
		}
	}
}