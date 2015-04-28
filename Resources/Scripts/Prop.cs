using UnityEngine;
using System.Collections;

public class Prop : MonoBehaviour {

	public bool isDoor;
	private float prevOpenTime;
	private Bear bearScript;
	private const float suspicionIncreaseUponCollision = 5f;

	// Use this for initialization
	void Start () {
		prevOpenTime = -100f;
		bearScript = (GameObject.Find ("Bear")).GetComponent<Bear>();
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name.Equals("Bear") && Time.time - prevOpenTime > 1f)
		{
			GetComponent<AudioSource>().Play();
			prevOpenTime = Time.time;

			if(!isDoor)
			{
				bearScript.IncreaseSuspicion(suspicionIncreaseUponCollision);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
