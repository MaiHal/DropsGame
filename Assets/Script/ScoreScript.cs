using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
	public Text scoreText;
	public int score;
	// Use this for initialization
	void Start () {
		score = 0;
		countScore ();
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = score.ToString();
	}

	void countScore(){
		score = 10;
	}
}
