using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

	public static int StartingScore = 0;
	private int score;

	public int levelTime = 60; // in s
	public bool runTimer = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(runTimer) {
			levelTime -= Time.deltaTime;
			if (levelTime <= 0) {
				EndLevel("fail");
			}
		}
	}

	void OnGUI () {
		// see https://github.com/fbsamples/friendsmash-unity/blob/master/friendsmash_complete/Assets/Scripts/GameStateDisplay.cs
		// for how to display the time remaining, if you want to do it with a GUI. Else manipulate a sprite in Update().
	}

	public void EndLevel(string result) {
		if (result == "fail") {

		} else if (result == "succeed") {

		}
	}


}
