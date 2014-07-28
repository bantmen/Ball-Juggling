using UnityEngine;
using System.Collections;
using System.Collections.Generic;  //to use lists
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

	public byte playLoud;  //2 loudest 0 softest

	void Start () {
		GameObject go = GameObject.Find ("Player");
		script = go.GetComponent <PlayerMovement> ();

		gameOver = false;
		sayOnce = true;

		score = 0;
		temp_kickCount = 0;

		if (playLoud == 2) audio.volume = 0.300f;
		else if (playLoud == 1) audio.volume = 0.175f;
		else audio.volume = 0.050f;
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
				int scoreInt = (int) score;
				UnityEngine.Debug.Log("score is: ");
				UnityEngine.Debug.Log(scoreInt);
				string scoreStr = scoreInt.ToString();
				HighScoreSet(scoreInt);
				Talk ("Your score was" + scoreStr + "To continue the game press Q button");
				StartCoroutine(WaitTalk(6, "To listen to the highscores table press the W button. " +
					"To go to the tutorial screen press the Tab key"));
			}
			if (Input.GetKeyDown (KeyCode.W)) {
				HighScoreListen();
			}
			if (Input.GetKeyDown (KeyCode.Q)) {
				Application.LoadLevel (1); 
			}
			else if (Input.GetKeyDown(KeyCode.Tab)) {
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
					audio.volume = 0.900f;
					audio.PlayOneShot(kick_hit_sound);
					if (playLoud == 2) audio.volume = 0.300f;
					else if (playLoud == 1) audio.volume = 0.175f;
					else audio.volume = 0.050f;
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

	void HighScoreSet (int score) {
		string [] highscoreArray = new string [5] {"High Score-1", "High Score-2", "High Score-3", "High Score-4", "High Score-5"}; 
		List<int> scoreList = new List<int>();
		int count = 0;
		while (count != 5){
			scoreList.Add(PlayerPrefs.GetInt(highscoreArray[count]));
			count += 1;
		}
		scoreList.Add (score);
		scoreList.Sort ();
		scoreList.Reverse ();   //for reversed sort effect
		scoreList.RemoveAt (5); //remove the last item in the list
		count = 0;
		while (count != 5) {
			PlayerPrefs.SetInt(highscoreArray[count], scoreList[count]);
			count += 1;
		}
	}

	void HighScoreListen () {
		string [] highscoreArray = new string [5] {"High Score-1", "High Score-2", "High Score-3", "High Score-4", "High Score-5"}; 
		string highscoreString = "";
		int count = 0;
		while (count != 5) {
			if (count != 4) highscoreString += PlayerPrefs.GetInt(highscoreArray[count]) + ", ";
			else highscoreString += "and " + PlayerPrefs.GetInt(highscoreArray[count]);
			count += 1;
		}
		Talk ("The top 5 highscores are " + highscoreString);
	}

	void Talk (string message) {
		System.Diagnostics.Process.Start ("say", message);
	}

	IEnumerator WaitTalk (int sec, string message){
		yield return new WaitForSeconds (sec);
		Talk (message);
	}
	
}
