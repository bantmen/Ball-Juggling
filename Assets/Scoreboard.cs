using UnityEngine;
using System.Collections;

public class Scoreboard : MonoBehaviour {

	void Awake () {
		PlayerPrefs.SetFloat ("High Score-1", 5);
		PlayerPrefs.SetFloat ("High Score-2", 4);
		PlayerPrefs.SetFloat ("High Score-3", 3);
		PlayerPrefs.SetFloat ("High Score-4", 2);
		PlayerPrefs.SetFloat ("High Score-5", 1);

	}
	
	void Update () {
	
	}
}
