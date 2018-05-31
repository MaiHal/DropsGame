using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDrops : MonoBehaviour {
	GenerateCube gc;
	DeleteByAdd dba;
	int k;
	int l;

	// Use this for initialization
	void Start () {
		gc = GetComponent<GenerateCube>();
		dba = GetComponent<DeleteByAdd> ();
	}

	// Update is called once per frame
	void Update () {
	}

	public void generateAddDrops(int i, bool flag){
		int r;
		int rest = gc.cube [i].Count;
		float initPos;

		if (flag == true) {
			initPos = 17.5f;
		} else {
			initPos = 12.5f;
		}
		for(int j = 0; j < 16 - rest; j++){
			r = UnityEngine.Random.Range(0, 4);
			gc.strageDrops(r, i, j, initPos);
		}
		k = i;
		l = rest-6;
		Invoke ("waitLanding", 3.0f);
	}

	public void waitLanding(){
		dba.countColumnChain (k,l);
	}
}
