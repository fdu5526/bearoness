using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

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

	// bear runs into this NPC
	void OnCollisionEnter(Collision collision)
	{	

		// 2 legged bear run into player
		if(collision.gameObject.name.Equals("Bear") && bearScript.isOnTwoLegs)
		{
			bearScript.IncreaseSuspicion(5f);
			audios[0].Play();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
