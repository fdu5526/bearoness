using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {


	private float prevOpenTime;

	// Use this for initialization
	void Start () {
		prevOpenTime = -100f;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(Time.time - prevOpenTime > 1f)
		{
			GetComponent<AudioSource>().Play();
			prevOpenTime = Time.time;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
