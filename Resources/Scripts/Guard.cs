using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

	// the player
	private GameObject bear;
	private Bear bearScript;

	// how much a collision should increase suspicion by
	private const float suspicionIncreaseUponCollision = 10f;

	// audio
	private AudioSource[] audios;

	// Use this for initialization
	void Start () 
	{
		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();
		audios = GetComponents<AudioSource>();
	}

	// bear runs into this NPC
	void OnCollisionEnter(Collision collision)
	{	

		// bear run into guard
		if(collision.gameObject.name.Equals("Bear"))
		{
			bearScript.IncreaseSuspicion(suspicionIncreaseUponCollision);
			audios[0].Play();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
