using UnityEngine;
using System.Collections;

public class musicController : MonoBehaviour {
	private GameObject bear;
	private Bear bearScript;

	private AudioSource normMusic, detectMusic;
	private AudioSource[] audios;

	private bool normPlaying, detectPlaying;
	// Use this for initialization
	void Start () {
		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();

		AudioSource[] audios = GetComponents<AudioSource>(); 
		normMusic = audios[0];
		detectMusic = audios[1];
	}
	
	void musicCheck()
	{
		if (bearScript.isDiscovered)
		{
			if (!detectPlaying)
			{
				detectPlaying = true;
				normPlaying = false;
				detectMusic.Play();
				detectMusic.loop = true;
			}
		}

		else
		{
			if (!normPlaying)
			{
				normPlaying = true;
				detectPlaying = false;
				normMusic.Play();
				normMusic.loop = true;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		musicCheck();

		if (normPlaying == false)
		{
			normMusic.Stop();
			normMusic.loop = false;
		}

		if (detectPlaying == false)
		{
			detectMusic.Stop();
			detectMusic.loop = false;
		}
	}
}
