using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

	// the player
	private GameObject bear;
	private Bear bearScript;

	// the detection circle
	private GameObject detectionCircle;

	// when guard is suspicious, and when to get back up after getting hit
	private float prevSuspicionTime;
	private const float suspicionCooldown = 0.3f;
	private const float getUpTime = 1f;

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

		detectionCircle = Instantiate ((GameObject)(Resources.Load("Prefabs/GuardDetectionCircle")));
	}

	// bear runs into this NPC
	void OnCollisionEnter(Collision collision)
	{	

		// bear run into guard
		if(collision.gameObject.name.Equals("Bear"))
		{
			if(Time.time - prevSuspicionTime > suspicionCooldown)
			{
				bearScript.IncreaseSuspicion(suspicionIncreaseUponCollision);
				audios[0].Play();
				prevSuspicionTime = Time.time;
			}
		}
	}

	// put the detection circle where the guard is
	private void SetDetectionCirclePosition()
	{
		Vector3 v = GetComponent<Transform>().position;
		float y = detectionCircle.GetComponent<Transform>().position.y;
		detectionCircle.GetComponent<Transform>().position = new Vector3(v.x, y, v.z);
	}
	
	// Update is called once per frame
	void Update () 
	{
		SetDetectionCirclePosition();
		

		// get back up after getting hit
		if(Time.time - prevSuspicionTime > getUpTime)
		{
			GetComponent<Transform>().eulerAngles = Vector3.zero;
		}
	}
}
