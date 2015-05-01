using UnityEngine;
using System.Collections;

public class MovieEnding : MonoBehaviour {
	
	private MovieTexture movTexture;
	private bool showCredits;

	// Use this for initialization
	void Start () 
	{
		showCredits = false;
		movTexture = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
		movTexture.Play();
		GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!movTexture.isPlaying && !showCredits)
		{
			GetComponent<Renderer>().material = Resources.Load("UI/Materials/Credits", typeof(Material)) as Material;
			showCredits = true;
		}

		if(showCredits && Input.anyKey)
		{
			Application.Quit();
		}
	}
}
