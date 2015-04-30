using UnityEngine;
using System.Collections;

public class Backstory : MonoBehaviour {
	public bool shouldMoveOnToLevel;
	public int nextLevelNumber;

	// Use this for initialization
	void Start () 
	{
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
		{
			gameObject.SetActive(false);
			Time.timeScale = 1;

			if(shouldMoveOnToLevel)
			{
				Application.LoadLevel ("level" + nextLevelNumber);
			}

		}
	}
}
