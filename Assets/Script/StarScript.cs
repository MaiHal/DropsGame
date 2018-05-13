using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour {
	GameObject star;
	GameObject lightingStar;
	GameObject bar;
	float lineX;
	float barX;

	// Use this for initialization
	void Start () {
		star = transform.Find ("Star").gameObject;
		lightingStar = transform.Find ("LightingStar").gameObject;
		bar = GameObject.Find ("ScoreImage");

		star.SetActive (true);
		lightingStar.SetActive (false);
		lineX = GetComponent<RectTransform>().anchoredPosition.x;
	}

	// Update is called once per frame
	void Update () {
		starCheck ();
	}

	void starCheck(){
		barX = bar.GetComponent<RectTransform>().sizeDelta.x/2 + bar.GetComponent<RectTransform>().anchoredPosition.x;
		if (barX >= lineX) {
			star.SetActive (false);
			lightingStar.SetActive (true);
		}
	}
}
