using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour {
	LineRenderer scoreBar;
	// Use this for initialization
	void Start () {
		scoreBar = GameObject.Find("Score").GetComponent<LineRenderer>();
		float x = -10.0f;
		Vector3 point = new Vector3(x, 3.2f, 0);
		//-6 ~ -11.3
		scoreBar.SetPosition(1, point);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
