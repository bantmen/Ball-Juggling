using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class BallMovement : MonoBehaviour {

	Vector3 velocity;
	public Vector3 gravity;  //adjust with the difficulty
	public float epsilon = 0.003f;    //position interval in which our guy can kick the ball, adjust with difficulty

	PlayerMovement script;
	bool kicked = false;

	public bool gameOver;

	public int score = 0;

	bool sayOnce;
	public AudioClip game_over;

	void Start () {
		GameObject go = GameObject.Find ("Player");
		script = go.GetComponent <PlayerMovement> ();

		gameOver = false;
		sayOnce = true;
	}

	void Update () {
		if (script.didKick) {
			kicked = true;
		}
		if (transform.position.y < -0.8612219) {
			if (sayOnce){
				audio.Stop ();
				gameOver = true;
				audio.PlayOneShot(game_over);
				sayOnce = false;
				System.Diagnostics.Process.Start ("say", "To continue the game press Space key   " +
					    "To go to the tutorial screen press the Tab key");
			}
			if (Input.GetKeyDown ("space") || Input.GetKeyDown ("left ctrl")) {
				Application.LoadLevel (1); 
			}
			if (Input.GetKeyDown(KeyCode.Tab)) {
				Application.LoadLevel(0);
			}
		}
	}

	void FixedUpdate () {
		if (!gameOver){
			if (kicked) {
				kicked = false;
				if (-epsilon < transform.position.y && epsilon > transform.position.y){ //accepted interval of kicking 
					//play the audio
					audio.Stop ();
					audio.Play ();
					velocity.y = Random.Range (4.5f, 7.5f);
					score += 1;
				}
			}
			Vector3 temp = transform.position;
			velocity -= gravity;
			temp += velocity * Time.deltaTime;
			transform.position = temp;
		}
	}
}
