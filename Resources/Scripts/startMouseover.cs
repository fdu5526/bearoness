using UnityEngine;
using System.Collections;

public class startMouseover : MonoBehaviour {

	public Sprite start0, start1;

	// Use this for initialization
	void Start () {
		//start0 = Resources.Load<Sprite>("Sprites/startblack");
		//start1 = Resources.Load<Sprite>("Sprites/startwhite");
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
