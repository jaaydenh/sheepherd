using UnityEngine;
using System.Collections;

public class GhostMove : MonoBehaviour {
    public Transform[] waypoints;
    int cur = 0;

	public GameObject[] herders;
	public GameObject[] sheep;

	// public GameObject centroidMarker;

	public float fearError = 0.0001f;
	public float flockError = 4.0f;

	public float fearScale = 1.0f;
	public float flockScale = 0.25f;

	public float fearRadius = 16.0f;
	public float flockRadius = 16.0f;

	void Start() {
		herders = GameObject.FindGameObjectsWithTag("herder");

	}

    public float speed = 0.01f;

	// if we had done this right - applying forces to physics objects - it would have been easier to implement
	// http://gamedevelopment.tutsplus.com/series/understanding-steering-behaviors--gamedev-12732
		
    void FixedUpdate () {	
		sheep = GameObject.FindGameObjectsWithTag("sheep");

		Vector2 fearVector = Vector2.zero; // run away from the herders * their closeness
		foreach(GameObject h in herders) {

			Vector2 between = transform.position - h.transform.position;

			if(between.magnitude < 16) {

				// normalize the vector
				Vector2 sheepToHerder = transform.position - h.transform.position; sheepToHerder /= sheepToHerder.magnitude;

				// scale it proportional to 1/distance^2, but make sure we never deal with distance=0
				// distance^2 means that when we're very close, we overcome any amount of competing influence
				float useDistance = Mathf.Max (sheepToHerder.magnitude, fearError);
				float closeness = (1/ (useDistance * useDistance * 15));
				sheepToHerder *= closeness;

				// and add it to the running sum
				fearVector += (sheepToHerder * fearScale);
			}
		}

		Vector2 flockVector = Vector2.zero; // run toward the centroid of the sheep / closeness
		Vector2 centroid = Vector2.zero;
		int localCount = 0;
		foreach(GameObject s in sheep) {
			Vector2 path = (Vector2)s.transform.position - (Vector2)transform.position;
			float distance = path.magnitude;
			if (distance <= flockRadius) {
				centroid += (Vector2)s.transform.position;
				++localCount;
			}
		}
		centroid /= localCount; // where the average sheep is, within the flock radius

		// centroidMarker.transform.position = centroid;

		// now take a path toward the centroid, but stop if we're close enough (<error)
		Vector2 sheepToCentroid = centroid - (Vector2)transform.position;
		if(sheepToCentroid.magnitude > flockError) {
			float contentness = (1 / sheepToCentroid.magnitude);
			contentness = 1; // while we deug this behavior
			sheepToCentroid /= sheepToCentroid.magnitude;
			flockVector = sheepToCentroid * contentness;
		}

		flockVector *= flockScale;

		Vector2 netVector = flockVector + fearVector;

		Vector2 target = Vector2.MoveTowards(transform.position, (Vector2)transform.position + netVector, speed);

//		Debug.DrawLine (transform.position, ((Vector2)transform.position + fearVector), Color.red, 0, false);
//		Debug.DrawLine (transform.position, ((Vector2)transform.position + flockVector), Color.yellow, 0, false);
//		Debug.DrawLine (transform.position, 0.5f*((Vector2)transform.position + netVector), Color.blue, 0, false);
		
		rigidbody2D.MovePosition(target);

        // Animation
//		Vector2 dir = netVector;
//        GetComponent<Animator>().SetFloat("DirX", dir.x);
//        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

}
