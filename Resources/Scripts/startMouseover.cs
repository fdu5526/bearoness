using UnityEngine;
using System.Collections;

public class startMouseover : MonoBehaviour {

	Sprite start0, start1;

	// Use this for initialization
	void Start () {
		start0 = Resources.Load<Sprite>("Sprites/start");
		start1 = Resources.Load<Sprite>("Sprites/start_mouseover");
	}

	void OnMouseEnter(){
		gameObject.GetComponent<SpriteRenderer>().sprite = start1;
	}

	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer>().sprite = start0;
	}

	void OnMouseDown(){
		Application.LoadLevel("level1");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
