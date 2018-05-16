using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDrops : MonoBehaviour {
	GenerateCube gc;
	// Use this for initialization
	void Start () {
		gc = GetComponent<GenerateCube>();
	}
	
	// Update is called once per frame
	void Update () {
		generateAddDrops ();
	}

	public void generateAddDrops(){
		int r;
		for(int i = 0; i < 10; i++){
			int lost = gc.cube [i].Count;
			for(int j = 0; j < 10 - lost; j++){
				r = UnityEngine.Random.Range(0, 4);
				gc.strageDrops(r, i, j, 7.5f);
			}
		}
	}
}
