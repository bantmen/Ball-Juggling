using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	Animator animator;
	public bool didKick = false;

	BallMovement script;

	GameObject go;
	GameObject go2;

	public AudioClip kick;

	void Start () {
		go = GameObject.Find ("Player");
		animator = go.GetComponent<Animator> ();

		go2 = GameObject.Find ("ball");
		script = go2.GetComponent<BallMovement> ();
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || Input.GetKeyDown("left ctrl")) {
			didKick = true;
			script.temp_kickCount += 1f;
		}
	}
	
	void FixedUpdate () {
		if (didKick) {
			animator.SetTrigger("Kicked");
			didKick = false;
		}

		if (script.kickLanded){
			script.kickLanded = false;
			//go2.audio.PlayOneShot(kick_hit_sound);
			audio.PlayOneShot(kick);
		}
	}
}
