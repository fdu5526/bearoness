using UnityEngine;
using System.Collections;

public class MoveOntoLevel2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name.Equals("Bear"))
		{
			Application.LoadLevel ("level2");
		}
	}
	

}
