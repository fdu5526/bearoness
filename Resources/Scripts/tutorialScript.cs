using UnityEngine;
using System.Collections;

public class tutorialScript : MonoBehaviour {
	private GameObject bear,twoLegsBox, detectedBox;
	private Bear bearScript;

	private bool isTriggered, isDiscovered, alreadyDiscovered;


	// Use this for initialization
	void Start () {
		bear = GameObject.Find("Bear");
		twoLegsBox = GameObject.Find("TwoLegsBox");
		detectedBox = GameObject.Find("DetectedBox");
		bearScript = bear.GetComponent<Bear>();
		isTriggered = false;
		alreadyDiscovered = false;
		twoLegsBox.SetActive(false);
		detectedBox.SetActive(false);

	}

	void OnTriggerEnter(Collider collision)
	{
		if(collision.gameObject.name.Equals("Bear"))
		{
			isTriggered = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		isDiscovered = GameObject.Find ("Bear").GetComponent<Bear>().isDiscovered;

		if (isDiscovered && alreadyDiscovered == false)
		{
			detectedBox.SetActive(true);
			Time.timeScale = 0;
			alreadyDiscovered = true;
		}

		if (isTriggered)
		{
			twoLegsBox.SetActive(true);
			Time.timeScale = 0;
		}

		if (Input.GetKeyDown("space") && twoLegsBox.activeInHierarchy)
		{
			twoLegsBox.SetActive(false);
			isTriggered = false;
			Destroy(this);
			Time.timeScale = 1;
		}

		if (Input.GetKeyDown("space") && detectedBox.activeInHierarchy)
		{
			detectedBox.SetActive(false);
			Destroy(detectedBox);
			Time.timeScale = 1;
		}


	}
}
