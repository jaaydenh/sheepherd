using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

	public static int StartingScore = 0;
	private int score;

	public int levelTime = 30; // in s
	private float currentTime;
	public bool runTimer = false;

	public GameObject timer;

	// Use this for initialization
	void Start () {
		currentTime = levelTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(runTimer) {
			//currentTime -= Mathf.RoundToInt(Time.time);
			currentTime = levelTime - Time.time;
			timer = GameObject.Find("timer_bar_fill");
			float percentage = (currentTime / levelTime) * 4.0f;
			timer.transform.localScale = new Vector3(percentage,1.0f);
			if (currentTime <= 0) {
				EndLevel("fail");
			}
		}
	}

	void OnGUI () {
		// see https://github.com/fbsamples/friendsmash-unity/blob/master/friendsmash_complete/Assets/Scripts/GameStateDisplay.cs
		// for how to display the time remaining, if you want to do it with a GUI. Else manipulate a sprite in Update().
	}

	public void EndLevel(string result) {
		runTimer = false;
		if (result == "fail") {
			print ("fail");
		} else if (result == "succeed") {
			print ("succeed");
		}
	}


}
