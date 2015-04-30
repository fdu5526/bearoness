using UnityEngine;
using System.Collections;

public class MoveOntoLevel2 : MonoBehaviour {

	private Bear bearScript;

	// Use this for initialization
	void Start () 
	{
		bearScript = GameObject.Find ("Bear").GetComponent<Bear>();
	}

	void OnCollisionEnter(Collision collision)
	{
		if(!bearScript.isDiscovered &&
			collision.gameObject.name.Equals("Bear"))
		{
			Application.LoadLevel ("EatingContest");
		}
	}
	

}
