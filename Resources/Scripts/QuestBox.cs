using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestBox : MonoBehaviour {

	Text questText;
	string activeQuest = "Go to the Ballroom";

	// Use this for initialization
	void Start () {
		questText = this.gameObject.GetComponentInChildren<Text>();
		questText.text = activeQuest;
	}
	
	void ChangeQuest(string s){
		questText.text = s;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
