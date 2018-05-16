using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDelete : MonoBehaviour {
	GameObject gameManager;
	GenerateCube gc;
	public int i;
	public int j;
	float timeleft;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		gc = gameManager.GetComponent<GenerateCube>();
		initPos ();
		//Debug.Log (i+","+j);
	}
	
	// Update is called once per frame
	void Update () {
		initPos ();
		timeleft += Time.deltaTime;
		if (timeleft >= 0.2f) {
			timeleft = 0.0f;
			checkColor();
		}
	}

	public void initPos(){
			i = (int)Mathf.Round(4.5f - transform.position.x);
			j = (int)Mathf.Round(4.5f + transform.position.y);
	}

	void checkColor(){
		if (j+2 < gc.cube[i].Count) {
			if (transform.tag == gc.cube [i] [j + 1].tag) {
				if (transform.tag == gc.cube [i] [j + 2].tag) {
					for (int k = 0; k < 3; k++) {
						gc.cube [i] [j + 2 - k].SetActive(false);
						//Destroy(gc.cube [i] [j + 2 - k]);
						gc.cube [i].RemoveAt (j + 2 - k);
						Debug.Log ("Destroy:cube["+i + "][" + (j + 2 - k)+"]");
					}
				}
			} 
		}
	}
}
