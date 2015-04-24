using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestBox : MonoBehaviour {

	private Sprite regbox, suspbox;
	private GameObject bear, questBox;
	private Bear bearScript;
	Text questText;
	string activeQuest = "Go to the Ballroom";

	// Use this for initialization
	void Start () {
		bear = GameObject.Find ("Bear");
		questBox = GameObject.Find("Quest");
		bearScript = bear.GetComponent<Bear>();
		regbox = Resources.Load<Sprite>("UI/questbox");
		suspbox = Resources.Load<Sprite>("UI/questbox_detected");
		questText = this.gameObject.GetComponentInChildren<Text>();
		questText.text = activeQuest;

		gameObject.GetComponent<SpriteRenderer>().sprite = regbox;
	}
	
	void ChangeQuest(string s){
		questText.text = s;
	}
	
	// Update is called once per frame
	void Update () {
		if (bearScript.isDiscovered)
		{
			questBox.GetComponentInChildren<Image>().sprite = suspbox;
			ChangeQuest("Find a Changing Room!");
		}
	}
}
