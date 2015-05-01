using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Eating : MonoBehaviour {

	public bool started = false;
	public bool won = false;
	public bool lose = false;
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


	private float lastIncreaseTime;
	private const float increaseCooldown = 0.01f;

	private float maxRudeness = 100f;
	public float rudeness;
	private float politenessGained = -4f;

	private AudioSource[] audios;


	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 30;
		bearModel = GameObject.Find("Bear").GetComponent<Transform>().Find("bearModel").gameObject;
		anim = bearModel.GetComponent<Animator>();
		lastKeyPressed = 0f;
		rudeness = 0f;
		loseText.text = startString;
		losePanel.SetActive(true);

		suspicionSlider = GameObject.Find("UI").GetComponent<Transform>().Find("SuspicionMeter").gameObject.GetComponent<Slider>();

		won = false;
		started = false;

		audios = GetComponents<AudioSource>();
	}


	void Lose()
	{
		loseText.text = loseString;
		losePanel.SetActive(true);
		lose = true;
		audios[0].Play();
		audios[1].Stop();
	}

	void Win()
	{
		loseText.text = winString;
		losePanel.SetActive(true);
		audios[1].Stop();
		won = true;
	}

	// decrease rudeness
	void CheckControl(){

		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)){
			lastKeyPressed = Time.timeSinceLevelLoad;
			anim.speed = Mathf.MoveTowards(anim.speed, 0.2f, 0.15f);
			float newVal = rudeness + politenessGained;
			rudeness = (0 > newVal) ? 0 : newVal;
			suspicionSlider.value = rudeness;
		}

	}
	// increase rudeness automatically
	void IncreaseSlider()
	{
		rudeness += (Time.timeSinceLevelLoad - startTime)/contestLength * 0.4f + 0.7f;
		suspicionSlider.value = rudeness;
		anim.speed = rudeness/maxRudeness * 5f + 1f;
	}
	
	// Update is called once per frame
	void Update () {
		
		// play the eating game
		if(started && (!lose && !won))
		{

			audios[1].pitch = rudeness/maxRudeness * 3f + 1f;

			if(rudeness >= 99.9f)
			{
				Lose ();
			}
			
			else if(Time.timeSinceLevelLoad >= startTime + contestLength)
			{
				Win ();
			}
			
			else
			{
				CheckControl();

				if(Time.time - lastIncreaseTime > increaseCooldown)
				{
					IncreaseSlider();
					lastIncreaseTime = Time.time;
				}
			}

		}
		// start eating
		else if(!started && Input.GetKeyDown(KeyCode.Space))
		{
			started = true;
			audios[1].Play();
			startTime = Time.timeSinceLevelLoad;
		}
		// lost
		else if (lose && Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("EatingContest");
		}
		// won
		else if (won && Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("level2");
		}
	
	}
}
