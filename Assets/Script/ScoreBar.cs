using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour {
	LineRenderer scoreBar;
	GameObject gameManager;
	ScoreScript ss;
	Vector3 point;
	//スコアにかける重み
	float weight = 0.001f;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		ss = gameManager.GetComponent<ScoreScript>();
		scoreBar = GameObject.Find("Score").GetComponent<LineRenderer>();
		point = new Vector3(-6.0f, 3.2f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		float tmp = -6.0f - ss.score * weight;
		if (-11.3f <= tmp) {
			point.x = -6.0f - ss.score * weight;
		} else {
			point.x = -11.3f;
		}
		//-6 ~ -11.3
		scoreBar.SetPosition(1, point);
	}
}
