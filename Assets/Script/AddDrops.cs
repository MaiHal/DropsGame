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
			for(int j = 0; j < 10 - (gc.cube[i].Count); j++){
				r = UnityEngine.Random.Range(0, 4);
				gc.strageDrops(r, i);
				gc.cube[i][j] = Instantiate (gc.cube[i][j], new Vector3( 4.5f - i * 1.0f, 7.5f + j * 1.0f, 0),  Quaternion.identity) as GameObject;
			}
		}
	}
}
