using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	private Animator beamAnim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D co) {
		if (co.tag == "sheep") {
			GameObject sheep = co.gameObject;
			beamAnim = sheep.GetComponent<Animator>();	
			sheep.rigidbody2D.MovePosition(sheep.rigidbody2D.position);
			//sheep.rigidbody2D.velocity = new Vector2(0.0f,0.0f);
			beamAnim.Play("Beam");
			StartCoroutine(WaitThenDoThings(co.gameObject));
			//Destroy(co.gameObject);
			//GameObject savedSheep = Instantiate(

		}
	}

	IEnumerator WaitThenDoThings(GameObject obj)
	{
		yield return new WaitForSeconds(1.0f);
		
		// Now do some stuff...
		Destroy(obj);

		//Debug.Log("Next Animation");
	}
}
