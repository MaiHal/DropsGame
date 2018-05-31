using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteByChange : MonoBehaviour {
	GenerateCube gc;
	ScoreScript sc;
	AddDrops ad;
	DeleteByDelete dbd;
	int k;
	int l;
	bool flag = false;

	// Use this for initialization
	void Start () {
		gc = GetComponent<GenerateCube>();
		sc = GetComponent<ScoreScript> ();
		ad = GetComponent<AddDrops> ();
		dbd = GetComponent<DeleteByDelete> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public int checkUpColColor(int i, int j, int colChain,int count, int rec){
		if (j + count <= 9) {
			if (gc.cube [i] [j].tag == gc.cube [i] [j + count].tag) {
				colChain = colChain + 1;
				if (count + 1 < rec) {
					return checkUpColColor (i, j, colChain, count + 1, rec);
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

	public int checkDownColColor(int i, int j, int colChain,int count, int rec){
		if (0 <= j - count) {
			if (gc.cube [i] [j].tag == gc.cube [i] [j - count].tag) {
				colChain = colChain + 1;
				if (count + 1 < rec) {
					return checkDownColColor (i, j, colChain, count + 1, rec);
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

	public int checkRightRowColor(int i, int j, int colChain,int count, int rec){
		if (i + count <= 9) {
			if (gc.cube [i] [j].tag == gc.cube [i+count] [j].tag) {
				colChain = colChain + 1;
				if (count + 1 < rec) {
					return checkRightRowColor (i, j, colChain, count + 1, rec);
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

	public int checkLeftRowColor(int i, int j, int colChain,int count, int rec){
		if (0<= i - count) {
			if (gc.cube [i] [j].tag == gc.cube [i-count] [j].tag) {
				colChain = colChain + 1;
				if (count + 1 < rec) {
					return checkLeftRowColor (i, j, colChain, count + 1, rec);
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

	public bool countColumnChain(int i, int j, int up, int down, bool flag){
		int colChain = 1;
		int upColChain = 1;
		int downColChain = 1;
		int beginDrop = j;

		upColChain = checkUpColColor (i, j, upColChain, 1, up+1);
		downColChain = checkDownColColor (i, j, downColChain, 1, down+1);

		beginDrop = j - (downColChain - 1);
		colChain = downColChain + upColChain - 1;
	
		//チェーンデータをもとにdelete
		if (colChain >= 3) {
			//deleteColumnChain (i, colChain, beginDrop, flag);
			return true;
		} else {
			return false;
		}
	}

	public bool countRowChain(int i, int j, int right, int left, bool flag){
		int rowChain = 1;
		int rightColChain = 1;
		int leftColChain = 1;
		int beginDrop = i;

		rightColChain = checkRightRowColor (i, j, rightColChain, 1, right+1);
		leftColChain = checkLeftRowColor (i, j, leftColChain, 1, left+1);

		beginDrop = i - (leftColChain - 1);
		rowChain = leftColChain + rightColChain - 1;

		//チェーンデータをもとにdelete
		if (rowChain >= 3) {
			//deleteRowChain (j, rowChain, beginDrop, flag);
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
		k = i;
		l = beginDrop;
		Invoke ("waitFallingDrops", 2.0f);
	}

	public void deleteRowChain(int j, int rowChain, int beginDrop, bool flag){
		for(int i = 0; i < rowChain; i++){
			gc.cube [beginDrop+rowChain-1-i][j].SetActive(false);
			gc.cube [beginDrop+rowChain-1-i].RemoveAt(j);
			ad.generateAddDrops (beginDrop+rowChain-1-i, flag);
		}
		sc.countScore (rowChain);
	}

	public void waitFallingDrops(){
		dbd.countColumnChain(k,l);
	}
}
