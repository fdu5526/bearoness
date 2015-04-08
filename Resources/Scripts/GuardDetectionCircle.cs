using UnityEngine;
using System.Collections;

public class GuardDetectionCircle : MonoBehaviour {

	// the player
	private GameObject bear;
	private Bear bearScript;

	// audio
	private AudioSource[] audios;

	// Use this for initialization
	void Start () 
	{
		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();
		audios = GetComponents<AudioSource>();
	}

	void OnTriggerStay(Collider collider)
	{	
		if(collider.CompareTag("Bear"))
		{
			if(bearScript.isOnTwoLegs)
			{
				bearScript.IncreaseSuspicion(0.2f);
			}
			else
			{
				bearScript.IncreaseSuspicion(1f);
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{

	}
}
