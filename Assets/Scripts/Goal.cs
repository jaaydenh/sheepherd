using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	private Animator beamAnim;
	private AudioSource beamSound;
	private GameObject[] sheeps;

	// Use this for initialization
	void Start () {
		sheeps = GameObject.FindGameObjectsWithTag("sheep");
		print (string.Format("sheeps: {0}",sheeps.Length));
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
			audio.Play();
			StartCoroutine(WaitThenDoThings(co.gameObject));
			//Destroy(co.gameObject);
		}
	}

	IEnumerator WaitThenDoThings(GameObject obj)
	{
		yield return new WaitForSeconds(1.0f);

		Destroy(obj);

		sheeps = GameObject.FindGameObjectsWithTag("sheep");
		print (string.Format("sheeps: {0}",sheeps.Length));
		if (sheeps.Length <= 1) {
			GameObject gsmHolder = GameObject.Find("GameStateManagerObject");
			GameStateManager gsm = gsmHolder.GetComponent<GameStateManager>();
			//GameObject gsm = GameObject.Find("GameStateManager");
			gsm.EndLevel("succeed");
			//gsm.GetComponent("GameStateManager").EndLevel("succeed");
//			GameStateManager.EndLevel("succeed");
		}
	}
}
