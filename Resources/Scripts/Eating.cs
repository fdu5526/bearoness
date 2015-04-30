using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Eating : MonoBehaviour {

	public bool started = false;
	public bool won = false;
	public string startString = "Use the A and D keys to eat politely. Only a princess with proper manners can meet a prince! Press the Spacebar to continue.";
	public string loseString = "You'll need to do better than that if you want to meet the prince! Guess you'll have to try again.";
	public string winString = "Excellent manners! That'll be sure to impress the prince!";
	public GameObject losePanel;
	public Text loseText;
	public float lastKeyPressed;
	public GameObject bearModel;
	public float contestLength = 15f;

	public Slider suspicionSlider;
	public Animator anim;
	private float startTime;

	private float maxRudeness = 100f;
	private float rudeness;
	private float politenessGained = -4f;



	// Use this for initialization
	void Start () {
		anim = bearModel.GetComponent<Animator>();
		lastKeyPressed = 0f;
		rudeness = 0f;
		loseText.text = startString;
		losePanel.SetActive(true);

		won = false;
		started = false;
	}

	void Reload(){
		Application.LoadLevel("level1");
	}

	void Lose(){
		loseText.text = loseString;
		losePanel.SetActive(true);
		Invoke ("Reload", 3);
	}

	void LoadNext(){
		Application.LoadLevel("level2");
	}

	void Win(){
		loseText.text = winString;
		losePanel.SetActive(true);
		won = true;
		Invoke ("LoadNext", 3);
	}

	void CheckControl(){

		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)){
			lastKeyPressed = Time.timeSinceLevelLoad;
			anim.speed = Mathf.MoveTowards(anim.speed, 0.2f, 0.15f);
			float newVal = rudeness + politenessGained;
			rudeness = (0 > newVal) ? 0 : newVal;
			suspicionSlider.value = rudeness;
		}

	}

	void IncreaseSlider(){
		rudeness = Mathf.MoveTowards(rudeness, maxRudeness, 0.7f);
		suspicionSlider.value = rudeness;
		anim.speed = Mathf.MoveTowards(anim.speed, 2.5f, 0.15f);
	}
	
	// Update is called once per frame
	void Update () {
		if(started){
			if(rudeness >= 99f){
				Lose ();
			}else if(Time.timeSinceLevelLoad >= startTime + contestLength){
				Win ();
			}else{
				CheckControl();
				IncreaseSlider();
			}

		}else if(!started && Input.GetKeyDown(KeyCode.Space)){
			started = true;
			startTime = Time.timeSinceLevelLoad;
			//anim.SetBool("Eating", true);
		
		}else if (won && Input.GetKeyDown(KeyCode.Space)){
			Application.LoadLevel("level2");
		}
	
	}
}
