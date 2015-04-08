using UnityEngine;
using System.Collections;

public class RegularNPC : MonoBehaviour {

	// the player
	private GameObject bear;
	private Bear bearScript;

	// for patrolling
	private float randomWalkCoodown;
	private float prevStartWalkTime;
	private bool isStopped;
	private const float maxSpeed = 3f;

	// audio
	private AudioSource[] audios;

	// Use this for initialization
	void Start () 
	{
		isStopped = Random.value >= 0.5f;
		prevStartWalkTime = 0f;

		bear = GameObject.Find ("Bear");
		bearScript = bear.GetComponent<Bear>();
		audios = GetComponents<AudioSource>();
	}


	void OnCollisionEnter(Collision collision)
	{	

		// 2 legged bear run into player
		if(collision.gameObject.name.Equals("Bear") && bearScript.isOnTwoLegs)
		{
			bearScript.IncreaseSuspicion(10);
			audios[0].Play();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.time - prevStartWalkTime > randomWalkCoodown)
		{
			randomWalkCoodown = Random.Range(0.3f, 2f);
			prevStartWalkTime = Time.time;

			if(isStopped)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
			else
			{
				float x = Random.Range(-maxSpeed,maxSpeed);
				float z = Random.Range(-maxSpeed,maxSpeed);
				GetComponent<Rigidbody>().velocity = new Vector3(x,0f,z);
			}

			isStopped = !isStopped;
		}
	}
}
