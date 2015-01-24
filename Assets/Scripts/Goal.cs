using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D co) {
		if (co.tag == "sheep") {
			//GameObject sheep = co.gameObject;
			Destroy(co.gameObject);
			//GameObject savedSheep = Instantiate(

		}
	}
}
