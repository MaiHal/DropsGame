using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteByAdd : MonoBehaviour {

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

	public int checkColor(int i, int j, int colChain,int count, int rec){
		if (j + count <= 9) {
			if (gc.cube [i] [j].tag == gc.cube [i] [j + count].tag) {
				colChain = colChain + 1;
				if (count + 1 < rec) {
					return checkColor (i, j, colChain, count + 1, rec);
				} else {
					return colChain;
				}
			} else {
				return colChain;
			}
		} else {
			return colChain;
		}	
	}


	public void countColumnChain(int i, int rest){
		int colChain = 1;
		int j = rest;
		int beginDrop = j;
		int lost = 10 - rest;
		int upChain = 1;
		int upBeginDrop = 1;

		colChain = checkColor (i, j, colChain, 1, lost);

		upChain = colChain;

		if (lost >= 4 && upChain == 1) {
			upChain = checkColor (i, j + 1, upChain, 1, lost-1);
			upBeginDrop = j + 1;
		}

		if (lost == 5 && upChain == 1) {
			upChain = checkColor (i, j + 2, upChain, 1, lost - 2);
			upBeginDrop = j + 2;
		}



		//自分の下2つのドロップを探索
		if (j - 1 >= 0) {
			Debug.Log ("a");
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

		Debug.Log (upChain);
		if (upChain >= 3) {
			deleteColumnChain (i, upChain, upBeginDrop);
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

