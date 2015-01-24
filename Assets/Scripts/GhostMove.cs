using UnityEngine;
using System.Collections;

public class GhostMove : MonoBehaviour {
	public Transform[] waypoints;
	int cur = 0;
	
	public GameObject[] herders;
	public GameObject[] sheep;
	
	public float fearError = 0.1f;
	public float fearConstant = 1f;
	public float flockError = .1f;
	public float flockConstant = 1f;
	
	public float fearScale = 1.0f;
	public float flockScale = 0.25f;
	
	void Start() {
		herders = GameObject.FindGameObjectsWithTag("herder");
		sheep = GameObject.FindGameObjectsWithTag("sheep");
	}
	
	public float speed = 0.01f;
	
	// if we had done this right - applying forces to physics objects - it would have been easier to implement
	// http://gamedevelopment.tutsplus.com/series/understanding-steering-behaviors--gamedev-12732
	
	void FixedUpdate () {	
		Vector2 fearVector = Vector2.zero; // run away from the herders * their closeness
		foreach(GameObject h in herders) {
			float dx = transform.position.x - h.transform.position.x;
			float dy = transform.position.y - h.transform.position.y;
			
			// get a unit vector pointing in the right direction
			Vector2 sheepToHerder = new Vector2(dx, dy); sheepToHerder /= sheepToHerder.magnitude;
			
			// scale it proportional to 1/distance^2, but make sure we never deal with distance=0
			float useDistance = Mathf.Max (sheepToHerder.magnitude, fearError);
			float closeness = fearConstant * (1/ (useDistance * useDistance));
			sheepToHerder *= closeness;
			
			// and add it to the running sum
			fearVector += (sheepToHerder * fearScale);
		}
		
		Vector2 flockVector = Vector2.zero; // run toward the centroid of the sheep / closeness
		Vector2 centroid = Vector2.zero;
		foreach(GameObject s in sheep) {
			centroid += (Vector2)s.transform.position;
			
		}
		centroid /= sheep.Length; // where the average sheep is
		
		// now take a path toward the centroid, but stop if we're close enough (<error)
		Vector2 sheepToCentroid = centroid - (Vector2)transform.position;
		if(sheepToCentroid.magnitude > flockError) {
			float contentness = flockConstant * (1 / Mathf.Max (sheepToCentroid.magnitude, flockError));
			flockVector = sheepToCentroid * contentness;
		}
		
		Vector2 netVector = flockVector + fearVector;
		
		Vector2 target = Vector2.MoveTowards(transform.position, (Vector2)transform.position + netVector, speed);
		
		rigidbody2D.MovePosition(target);
		
		// Animation
		//		Vector2 dir = netVector;
		//        GetComponent<Animator>().SetFloat("DirX", dir.x);
		//        GetComponent<Animator>().SetFloat("DirY", dir.y);
	}
	
}
