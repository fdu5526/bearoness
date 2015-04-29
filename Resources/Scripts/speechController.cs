using UnityEngine;
using System.Collections;

public class speechController : MonoBehaviour {
	private GameObject bear;
	private Bear bearScript;

	private float suspicionPercent;

	private bool lowPlaying, midPlaying, highPlaying;

	private AudioSource low, mid, high;

	// Use this for initialization
	void Start () {
		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();

		AudioSource[] audios = GetComponents<AudioSource>(); 
		low = audios[0];
		mid = audios[1];
		high = audios[2];

		lowPlaying = false;
		midPlaying = false;
		highPlaying = false;

	}
	
	void speechCheck()
	{
		if (suspicionPercent < 25f)
		{
			if (!lowPlaying)
			{
				lowPlaying = true;
				midPlaying = false;
				highPlaying = false;
				low.Play();
				low.loop = true;
			}
		}

		else if (suspicionPercent > 25f && suspicionPercent < 50f)
		{
			if (!midPlaying)
			{
				lowPlaying = false;
				midPlaying = true;
				highPlaying = false;
				mid.Play();
				mid.loop = true;
			}
			
		}

		else if (suspicionPercent > 50f)
		{
			if (!highPlaying)
			{
				lowPlaying = false;
				midPlaying = false;
				highPlaying = true;
				high.Play();
				high.loop = true;
			}
		}
	}

	// Update is called once per frame
	void Update () {

		suspicionPercent = bearScript.suspicionPercent;

		speechCheck();
		if (lowPlaying == false)
		{
			low.Stop();
			low.loop = false;
		}
		if (midPlaying == false)
		{
			mid.Stop();
			mid.loop = false;
		}
		if (highPlaying == false)
		{
			high.Stop();
			high.loop = false;
		}
	}
}
