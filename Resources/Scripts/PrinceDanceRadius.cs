using UnityEngine;
using System.Collections;

public class PrinceDanceRadius : MonoBehaviour {
	private GameObject bear, prince;
	private Bear bearScript, princeScript;
	private bool isDancing, pressedE;

	private float defaultDiameter = 5f;

	private const float frameReduce = -0.05f;


	// Use this for initialization
	void Start () {
		isDancing = false;
		pressedE = false;
		bear = GameObject.Find("Bear");
		prince = GameObject.Find("Prince");
		bearScript = bear.GetComponent<Bear>();
		//princeScript = prince.GetComponent<Prince>();
	}

	void OnTriggerStay(Collider collider)
	{	
		// bear is within range
		if(collider.CompareTag("Bear"))
		{
			if (pressedE)
			{
				isDancing = true;
			}
		}

		else
		{
			if (isDancing){
				defaultDiameter += frameReduce;
			}
		}
	}

	void checkE()
	{
		if (Input.GetKeyDown("e") && pressedE == false){
			pressedE = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Transform>().localScale = new Vector3(defaultDiameter, 0.15f, defaultDiameter);
	}
}
