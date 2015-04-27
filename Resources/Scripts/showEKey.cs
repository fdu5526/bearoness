using UnityEngine;
using System.Collections;

public class showEKey : MonoBehaviour {

	private GameObject prince, eKey;
	private Prince princeScript;

	// Use this for initialization
	void Start () {
		prince = GameObject.Find("Prince");
		eKey = GameObject.Find("e key");
		princeScript = prince.GetComponent<Prince>();
	}
	
	// Update is called once per frame
	void Update () {
		if (princeScript.distanceClosed)
		{
			eKey.SetActive(true);
		}
		else
		{
			eKey.SetActive(false);
		}
	}
}
