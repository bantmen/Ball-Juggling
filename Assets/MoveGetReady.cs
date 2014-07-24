using UnityEngine;
using System.Collections;

public class MoveGetReady : MonoBehaviour {

	BallMovement script;
	float countdown = 1.5f;  //play the starting audio
	
	void Start () {
		GameObject go = GameObject.Find ("ball");
		script = go.GetComponent <BallMovement> ();
	}

	void FixedUpdate () {
		if (transform.position.z != -10 && Time.time > countdown) {
			UnityEngine.Debug.Log ("Declared");
			Vector3 temp = transform.position;
			temp.z = -10;
			transform.position = temp;
		}
	}
}


