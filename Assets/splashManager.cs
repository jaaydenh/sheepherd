using UnityEngine;
using System.Collections;

public class splashManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.X) || Input.GetButton ("P1x")) { 
			Application.LoadLevel("SheepyHerd");
		}
	}
}
