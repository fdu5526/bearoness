using UnityEngine;
using System.Collections;

public class Backstory : MonoBehaviour {
	private GameObject popup;
	public bool shouldMoveOnToLevel;
	public int nextLevelNumber;

	// Use this for initialization
	void Start () {
		//popup = GameObject.Find("Backstory pop-up");
		//Time.timeScale = 0;
		//popup.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
		{
			gameObject.SetActive(false);
			//Time.timeScale = 1;

			if(shouldMoveOnToLevel)
			{
				Application.LoadLevel ("level" + nextLevelNumber);
			}

		}
	}
}
