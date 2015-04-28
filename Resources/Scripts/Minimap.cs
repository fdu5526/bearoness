using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {

	private GameObject indicator;

	// Use this for initialization
	void Start () 
	{
		indicator = GetComponent<Transform>().Find("indicator").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
