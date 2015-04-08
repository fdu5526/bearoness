using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SuspicionIndicator : MonoBehaviour {

	public int suspicion;
	public Sprite blue, green, orange, red;
	Image image;

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
		image.sprite = blue;
	}
	
	// Update is called once per frame
	void Update () {
		// get suspicion from bear
		//suspicion = GameObject.Find ("Bear").GetComponent<Bear>().suspicion;
		
		// change icon appropriately
		if(suspicion >= 0 && suspicion < 25){
			image.sprite = blue;
		}else if (suspicion >= 25 && suspicion < 50){
			image.sprite = green;
		}else if (suspicion >= 50 && suspicion < 75){
			image.sprite = orange;
		}else {	
			image.sprite = red;
		}
	}
}
