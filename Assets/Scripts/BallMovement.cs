using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class BallMovement : MonoBehaviour {

	Vector3 velocity;
	public Vector3 gravity;  //adjust with the difficulty
	public float epsilon = 0.003f;    //position interval in which our guy can kick the ball, adjust with difficulty

	PlayerMovement script;
	bool kicked = false;
	public AudioClip kick_hit_sound;
	public AudioClip kick_swing_sound;

	public bool gameOver;

	public float score;
	public float temp_kickCount;

	bool sayOnce;
	public AudioClip game_over;

	public bool playLoud;

	void Start () {
		GameObject go = GameObject.Find ("Player");
		script = go.GetComponent <PlayerMovement> ();

		gameOver = false;
		sayOnce = true;

		score = 0;
		temp_kickCount = 0;

		if (playLoud) audio.volume = 0.30f;
		else audio.volume = 0.05f;
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
				Mathf.Round(score);
				System.Diagnostics.Process.Start ("say", "Your score was" + score +
				        "     To continue the game press Q button   " +
					    "To go to the tutorial screen press the Tab key");
			}
			if (Input.GetKeyDown (KeyCode.Q)) {
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
				if (-epsilon < transform.position.y && epsilon > transform.position.y){ //accepted interval
					//play the audio
					audio.Stop ();
					audio.Play ();
					velocity.y = Random.Range (4.5f, 7.5f);
					score += 100 / temp_kickCount;
					temp_kickCount = 0;
					audio.volume = 0.90f;
					audio.PlayOneShot(kick_hit_sound);
					if (playLoud) audio.volume = 0.30f;
					else audio.volume = 0.05f;
				}
				else{
					audio.PlayOneShot(kick_swing_sound);
					UnityEngine.Debug.Log("should play swing");
				}
			}
			Vector3 temp = transform.position;
			velocity -= gravity;
			temp += velocity * Time.deltaTime;
			transform.position = temp;
		}
	}
}
