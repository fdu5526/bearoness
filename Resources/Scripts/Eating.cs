using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Eating : MonoBehaviour {

	private bool started = false;
	private bool lost = false;
	private bool won = false;
	private string loseString = "You'll need to do better than that if you want to meet the prince! Press Space to try again.";
	private string winString = "Excellent manners! That'll be sure to impress the prince! Press Space to continue your quest!";
	public GameObject losePanel;
	public Text loseText;

	public GameObject bearModel;
	public Animator anim;
	private float startTime;

	private float maxRudeness = 100f;
	private float politenessGained = -10f;



	// Use this for initialization
	void Start () {
	
	}

	void Reload(){
		Application.LoadLevel(Application.loadedLevel);
	}

	void Lose(){
		loseText.text = loseString;
		losePanel.SetActive(true);
		lost = true;
	}

	void Win(){
		loseText.text = winString;
		losePanel.SetActive(true);
		won = true;
	}

	void CheckControl(){
		//bear.IncreaseSuspicion(10);

	}
	
	// Update is called once per frame
	void Update () {
		if(started){
			if(true){
				Lose ();
			}else if(Time.time >= startTime + 30f){
				Win ();
			}else{
				CheckControl();
			}

		}else if(!started && Input.GetKeyDown(KeyCode.Space)){
			started = true;
			startTime = Time.time;
		}else if(lost && Input.GetKeyDown(KeyCode.Space)){
			Reload ();
		}else if (won && Input.GetKeyDown(KeyCode.Space)){
			Application.LoadLevel("level2");
		}
	
	}
}
