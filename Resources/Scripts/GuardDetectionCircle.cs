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
		//audios = GetComponents<AudioSource>();
	}

	void OnTriggerStay(Collider collider)
	{	
		if(collider.name.Equals("Bear"))
		{
			print("sdfsdf");
		}
	}

	// Update is called once per frame
	void Update () 
	{

	}
}
