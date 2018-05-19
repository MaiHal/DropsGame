using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteByChange : MonoBehaviour {
	GenerateCube gc;
	ScoreScript sc;
	AddDrops ad;

	// Use this for initialization
	void Start () {
		gc = GetComponent<GenerateCube>();
		sc = GetComponent<ScoreScript> ();
		ad = GetComponent<AddDrops> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public bool countColumnChain(int i, int j, bool flag){
		int colChain = 1;
		int beginDrop = j;

		//自分の上2つのドロップを探索
		if (j +1 <= 9) {
			if (gc.cube [i] [j].tag == gc.cube [i] [j + 1].tag) {
				colChain += 1;
				if (j + 2 <= 9) {
					if (gc.cube [i] [j].tag == gc.cube [i] [j + 2].tag) {
						colChain += 1;
					}
				}
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
			deleteColumnChain (i, colChain, beginDrop, flag);
			return true;
		} else {
			return false;
		}
	}

	public void deleteColumnChain(int i, int colChain, int beginDrop, bool flag){
		for(int j = 0; j < colChain; j++){
			gc.cube [i][beginDrop+colChain-1-j].SetActive(false);
			gc.cube [i].RemoveAt(beginDrop + colChain-1 - j);
		}
		ad.generateAddDrops (i, flag);
		sc.countScore (colChain);
	}
}
