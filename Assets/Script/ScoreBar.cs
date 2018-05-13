using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour {
	GameObject gameManager;
	ScoreScript ss;
	Vector2 sd;
	Vector2 v;
	//スコアにかける重み
	float weight = 0.5f;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		ss = gameManager.GetComponent<ScoreScript>();
		sd = GetComponent<RectTransform>().sizeDelta;
		v = GetComponent<RectTransform> ().anchoredPosition;
		putInitBar ();
	}
	
	// Update is called once per frame
	void Update () {
		increaseBar ();
	}

	void putInitBar(){
		GetComponent<RectTransform>().sizeDelta = sd;
		v.x = sd.x / 2 - 87;
		GetComponent<RectTransform> ().anchoredPosition = v;
	}

	void increaseBar(){
		sd.x = ss.score * weight;
		v.x = sd.x / 2 - 87;

		if (sd.x <= 174) {
			GetComponent<RectTransform> ().sizeDelta = sd;
			GetComponent<RectTransform> ().anchoredPosition = v;
		} else {
			sd.x = 174;
			v.x = 0;
			GetComponent<RectTransform> ().sizeDelta = sd;
			GetComponent<RectTransform> ().anchoredPosition = v;
		}
	}
}
