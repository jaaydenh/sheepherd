using UnityEngine;
using System.Collections;

public class PacmanMove : MonoBehaviour {
    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;

	public KeyCode upKey, downKey, leftKey, rightKey;
	int sn = 0;

    void Start() {
        dest = transform.position;
		/* upKey = KeyCode.UpArrow;
		downKey = KeyCode.DownArrow;
		leftKey = KeyCode.LeftArrow;
		rightKey = KeyCode.RightArrow; */
	}

	void OnCollisionEnter2D() {
		//print ("OnCollisionEnter2D");
		//dest = (Vector2)transform.position;
	}

	void OnCollisionStay2D() {

//		if ((Vector2)transform.position != dest) {
//			print ("OnCollisionStay2D");
//			dest = (Vector2)transform.position;
//		}
	}

    void FixedUpdate() {
		//print (++sn + " " + gameObject.name);
		//print (++sn + " Pacman fixedupdate");
        // Move closer to Destination
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        rigidbody2D.MovePosition(p);
		float horizontal = 0;
		float vertical = 0;
		if (gameObject.name == "herder1") {
			horizontal = Input.GetAxis ("HorizontalP1");
			vertical = Input.GetAxis ("VerticalP1");
		} else if (gameObject.name == "herder2"){
			horizontal = Input.GetAxis ("HorizontalP2");
			vertical = Input.GetAxis ("VerticalP2");
		} else if (gameObject.name == "herder3"){
			horizontal = Input.GetAxis ("HorizontalP3");
			vertical = Input.GetAxis ("VerticalP3");
		} else if (gameObject.name == "herder4"){
			horizontal = Input.GetAxis ("HorizontalP4");
			vertical = Input.GetAxis ("VerticalP4");
		}
		//print ("axis6: " + axis6);
		//print ("axis7: " + axis7);
		// print ("input.getkey: " + Input.GetKey(upKey));

        // Check for Input if not moving
       // if ((Vector2)transform.position == dest) {

//		if (Input.GetAxis(upKey) && valid (Vector2.up)) {
//			dest = (Vector2)transform.position + Vector2.up;
//		}
		Vector2 input = new Vector2 ();
		input.x = horizontal;
		input.y = vertical;

		dest = (Vector2)transform.position + input;

//		if (vertical > 0 && valid (Vector2.up)) {
//			dest = (Vector2)transform.position + Vector2.up;
//		}
//		if (vertical < 0 && valid (-Vector2.up)) {
//			dest = (Vector2)transform.position - Vector2.up;
//		}
//		if (horizontal > 0 && valid (Vector2.right)) {
//			dest = (Vector2)transform.position + Vector2.right;
//		}
//		if (horizontal < 0 && valid (-Vector2.right)) {
//			dest = (Vector2)transform.position - Vector2.right;
//		}
//			if (Input.GetKey (upKey) && valid (Vector2.up)) {
//				dest = (Vector2)transform.position + Vector2.up;
//			}
//			if (Input.GetKey (rightKey) && valid (Vector2.right)) {
//				dest = (Vector2)transform.position + Vector2.right;
//			}
//			if (Input.GetKey (downKey) && valid (-Vector2.up)) {
//				dest = (Vector2)transform.position - Vector2.up;
//			}
//			if (Input.GetKey (leftKey) && valid (-Vector2.right)) {
//				dest = (Vector2)transform.position - Vector2.right;
//			}
							// else print(++sn + " no input");
				//} else {
			//dest = transform.position;
		//}

        // Animation Parameters
        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    bool valid(Vector2 dir) {
        // Cast Line from 'next to Pac-Man' to 'Pac-Man'
		// return true;
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
		//if (hit.collider != collider2D) print (gameObject.name + " collision detected, no move");
        return (hit.collider == collider2D);
    }
}
