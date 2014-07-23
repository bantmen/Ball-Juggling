using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	Animator animator;
	public bool didKick = false;

	void Start () {
		GameObject go = GameObject.Find ("Player");
		animator = go.GetComponent<Animator> ();
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || Input.GetKeyDown("left ctrl")) {
			didKick = true;
		}
	}
	
	void FixedUpdate () {
		if (didKick) {
			animator.SetTrigger("Kicked");
			didKick = false;
		}
		
	}
}
