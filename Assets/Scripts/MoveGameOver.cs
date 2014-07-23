using UnityEngine;
using System.Collections;

public class MoveGameOver : MonoBehaviour {

	BallMovement script;

	void Start () {
		GameObject go = GameObject.Find ("ball");
		script = go.GetComponent <BallMovement> ();
	}

	void FixedUpdate () {
		if (script.gameOver && transform.position.z != 0) {
			Vector3 temp = transform.position;
			temp.z = 0;
			transform.position = temp;
		}
	}
}
