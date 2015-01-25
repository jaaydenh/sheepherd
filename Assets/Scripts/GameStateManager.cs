using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {
	
	public static int StartingScore = 0;
	private int score;
	
	public string[] levelSequence;
	
	public int levelTime = 30; // in s
	private float currentTime;
	public bool runTimer = false;
	
	private int currLevel = 0;
	private int totalLevels = 2;
	public GameObject youLosePanel;
	public GameObject levelCompletePanel;
	private Animator levelCompleteAnimation;
	private Animator youLoseAnimation;
	
	private GameObject timer;
	private string stateOfGame;
	
	// Use this for initialization
	void Start () {
		stateOfGame = "go";
		currentTime = levelTime;
		runTimer = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(runTimer) {
			if (Input.GetButton("P1x")) {
				print("x button pressed");
			}

			currentTime = levelTime - Time.timeSinceLevelLoad;
			timer = GameObject.Find("timer_bar_fill");
			float percentage = (currentTime / levelTime) * 4.0f;
			timer.transform.localScale = new Vector3(percentage,1.0f);
			if (currentTime <= 0) {
				EndLevel("fail");
			}
			if(Input.GetKey (KeyCode.BackQuote)) { // cheat code to test winning
				EndLevel("succeed");
			}
		}
		if(stateOfGame == "fail") {
			if(Input.GetKey(KeyCode.X) || Input.GetButton("P1x")) {
				currentTime = levelTime;
				Application.LoadLevel(Application.loadedLevel);
				
				stateOfGame = "go";
				runTimer = true;
			}
		}
		if(stateOfGame == "succeed") {
			if(Input.GetKey(KeyCode.X) || Input.GetButton("P1x")) {
				currLevel= currLevel+1%levelSequence.Length;
				//if(currLevel < levelSequence.Length) {

				currentTime = levelTime;
				string nextLevel = levelSequence[currLevel];
				print(string.Format("going to level {0} {1}", currLevel+1, nextLevel));
				Application.LoadLevel(nextLevel);
				stateOfGame = "go";
				runTimer = true;
//				} else {
//					// out of levels...
//
//				}
				
			}
		}
	}
	
	//	void OnGUI () {
	// see https://github.com/fbsamples/friendsmash-unity/blob/master/friendsmash_complete/Assets/Scripts/GameStateDisplay.cs
	// for how to display the time remaining, if you want to do it with a GUI. Else manipulate a sprite in Update().
	//	}
	
	public void EndLevel(string result) {
		runTimer = false;
		stateOfGame = result;
		if (result == "fail") {
			GameObject youLosePanel = GameObject.Find("YouLosePanel");
			youLoseAnimation = youLosePanel.GetComponent<Animator>();
			youLoseAnimation.Play("GameOverClip");
		} else if (result == "succeed") {
			levelCompletePanel = GameObject.Find("LevelCompletePanel");
			levelCompleteAnimation = levelCompletePanel.GetComponent<Animator>();
			levelCompleteAnimation.Play("LevelComplete");
		}


	}
}
