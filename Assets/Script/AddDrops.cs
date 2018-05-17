using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDrops : MonoBehaviour {
	GenerateCube gc;
	int k;
	int l;

	// Use this for initialization
	void Start () {
		gc = GetComponent<GenerateCube>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void generateAddDrops(int i, bool flag){
		int r;
		int lost = gc.cube [i].Count;
		float initPos;

		if (flag == true) {
			initPos = 12.5f;
		} else {
			initPos = 7.5f;
		}
		for(int j = 0; j < 10 - lost; j++){
			r = UnityEngine.Random.Range(0, 4);
			gc.strageDrops(r, i, j, initPos);
		}
	}
}
