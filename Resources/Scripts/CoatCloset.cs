using UnityEngine;
using System.Collections;

public class CoatCloset : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.CompareTag("Bear"))
		{
			Application.LoadLevel ("level1");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
