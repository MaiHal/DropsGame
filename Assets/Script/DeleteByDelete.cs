using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteByDelete : MonoBehaviour {
	GenerateCube gc;
	ScoreScript sc;
	AddDrops ad;
	bool flag = false;

	// Use this for initialization
	void Start () {
		gc = GetComponent<GenerateCube>();
		sc = GetComponent<ScoreScript> ();
		ad = GetComponent<AddDrops> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void countColumnChain(int i, int rest){
		int colChain = 1;
		int j = rest;
		int beginDrop = j;

		//自分の上1つのドロップを探索
		if (j +1 <= 7) {
			if (gc.cube [i] [j].tag == gc.cube [i] [j + 1].tag) {
				colChain += 1;
			}
		}

		//自分の下2つのドロップを探索
		if (j - 1 >= 0) {
			if (gc.cube [i] [j].tag == gc.cube [i] [j - 1].tag) {
				colChain += 1;
				beginDrop = j - 1;
				if (j - 2 >= 0) {
					if (gc.cube [i] [j].tag == gc.cube [i] [j - 2].tag) {
						colChain += 1;
						beginDrop = j - 2;
					}
				}
			}
		}

		//チェーンデータをもとにdelete
		if (colChain >= 3) {
			deleteColumnChain (i, colChain, beginDrop);
			flag = true;
		} 
	}

	public void deleteColumnChain(int i, int colChain, int beginDrop){
		for(int j = 0; j < colChain; j++){
			gc.cube [i][beginDrop+colChain-1-j].SetActive(false);
			gc.cube [i].RemoveAt(beginDrop + colChain-1 - j);
		}
		ad.generateAddDrops (i, flag);
		sc.countScore (colChain);
	}
}
