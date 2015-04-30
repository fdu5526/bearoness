using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SuspicionIndicator : MonoBehaviour {

	public float suspicion;
	public Sprite blue, green, orange, red;
	Image image;

	private Bear bearScript;

	// Use this for initialization
	void Start () {
		suspicion = 0;
		image = this.gameObject.GetComponent<Image> ();
		/*
		blue = Resources.Load <Sprite> ("Images/bearicon2");
		green = Resources.Load <Sprite> ("Images/bearicon3");
		orange = Resources.Load <Sprite> ("Images/bearicon4");
		red = Resources.Load <Sprite> ("Images/bearicon-v2");
		*/

		bearScript = GameObject.Find ("Bear").GetComponent<Bear>();
		image.sprite = blue;
	}
	
	// Update is called once per frame
	void Update () {
		// get suspicion from bear
		suspicion = bearScript.suspicionPercent;
		
		// change icon appropriately
		if(suspicion >= 0f && suspicion < 25f){
			image.sprite = blue;
		}else if (suspicion >= 25f && suspicion < 50f){
			image.sprite = green;
		}else if (suspicion >= 50f && suspicion < 75f){
			image.sprite = orange;
		}else {	
			image.sprite = red;
		}
	}
}
