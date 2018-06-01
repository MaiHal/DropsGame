using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteByChange : MonoBehaviour {
	GenerateCube gc;
	ScoreScript sc;
	AddDrops ad;
	DeleteByDelete dbd;
	int[] colChain = new int[2];
	int[] rowChain = new int[2];
	int[] colBeginDrop = new int[2];
	int[] rowBeginDrop = new int[2];

	int n;
	int m;
	bool flag = false;

	bool[] columnFlag = new bool[2];
	bool[] rowFlag = new bool[2];
	bool deleteFlag = false;
	public int i,j,k,l;

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

	public void deleteManagement(string type){
		//changeによるdelete 
		if (type.Equals ("Change")) {
			//ドロップ位置が交換されるのを待つ
			Invoke ("change", 0.2f);
		}
	}

	public void change(){
		if (i == k) {
			//チェック
			if (j > l) {
				//列チェック　上半分と下半分
				columnFlag [0] = countColumnChain (i, j, 2, 0, 0, false);
				columnFlag [1] = countColumnChain (k, l, 0, 2, 1, columnFlag [0]);

				//行チェック
				rowFlag [0] = countRowChain (i, j, 2, 2, 0, false);
				rowFlag [1] = countRowChain (k, l, 2, 2, 1, rowFlag [0]);
			} else {
				columnFlag [0] = countColumnChain (k, l, 2, 0, 0, false);
				columnFlag [1] = countColumnChain (i, j, 0, 2, 1, columnFlag [0]);

				rowFlag [0] = countRowChain (k, l, 2, 2, 0, false);
				rowFlag [1] = countRowChain (i, j, 2, 2, 1, rowFlag [0]);
			}

			//delete
			if (columnFlag [0]) {
				//i(k)列目delete
				if (columnFlag [1]) {
					deleteColumnChain (i, colChain [0] + colChain [1], colBeginDrop [1], flag);
				} else {
					if (rowFlag [1]) {
						deleteColumnChain (i, colChain [0], colBeginDrop [0] - 1, flag);
					} else {
						deleteColumnChain (i, colChain [0], colBeginDrop [0], flag);
					}
				}

				//add時の調整もして
				//行delete
				if (rowFlag [0]) {
					deleteRowChain (System.Math.Max(j, l), rowChain [0], rowBeginDrop [0], flag);
				}
				//行delete
				if (rowFlag [1]) {
					deleteRowChain (System.Math.Min(j, l), rowChain [1], rowBeginDrop [1], flag);
				}
			} else {
				//add時の調整もして
				//行delete
				if (rowFlag [0]) {
					deleteRowChain (System.Math.Max(j, l), rowChain [0], rowBeginDrop [0], flag);
				}
				//行delete
				if (rowFlag [1]) {
					deleteRowChain (System.Math.Min(j, l), rowChain [1], rowBeginDrop [1], flag);
					if (columnFlag [1]) {
						deleteColumnChain (i, colChain [1] - 1, colBeginDrop [1], flag);
					}
				} else {
					if (columnFlag [1]) {
						deleteColumnChain (i, colChain [1], colBeginDrop [1], flag);
					}
				}
			}
		} else {
			//チェック
			if (k > i) {
				columnFlag [0] = countColumnChain (i, j, 2, 2, 0, false);
				columnFlag [1] = countColumnChain (k, l, 2, 2, 1, columnFlag [0]);

				//行チェック 右半分と左半分
				rowFlag [0] = countRowChain (i, j, 0, 2, 0, false);
				rowFlag [1] = countRowChain (k, l, 2, 0, 1, rowFlag [0]);
			} else {
				columnFlag [0] = countColumnChain (k, l, 2, 2, 0, false);
				columnFlag [1] = countColumnChain (i, j, 2, 2, 1, columnFlag [0]);

				rowFlag [0] = countRowChain (k, l, 0, 2, 0, false);
				rowFlag [1] = countRowChain (i, j, 2, 0, 1, rowFlag [0]);
			}

			//delete
			if (columnFlag [0]) {
				deleteColumnChain (System.Math.Min(i, k), colChain [0], colBeginDrop [0], flag);
				if (rowFlag [0]) {
					deleteRowChain (j, rowChain [0]-1, rowBeginDrop [0], flag);
				}
			} else {
				if (rowFlag [0]) {
					deleteRowChain (j, rowChain [0], rowBeginDrop [0], flag);
				}
			}
			if (columnFlag [1]) {
				deleteColumnChain (System.Math.Max (i, k), colChain [1], colBeginDrop [1], flag);
				if (rowFlag [1]) {
					deleteRowChain (j, rowChain [1] - 1, rowBeginDrop [1] + 1, flag);
				}
			} else {
				if (rowFlag [1]) {
					deleteRowChain (j, rowChain [1], rowBeginDrop [1], flag);
				}
			}		
		}
		if (columnFlag [0] == false && columnFlag [1] == false && rowFlag [0] == false && rowFlag [1] == false) {
			deleteFlag = false;
		} else {
			deleteFlag = true;
		}
		Debug.Log ("行"+rowFlag[0]+","+rowFlag[1]+" :列"+columnFlag[0]+","+columnFlag[1]);
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

	public bool countColumnChain(int i, int j, int up, int down, int n, bool flag){
		int upColChain = 1;
		int downColChain = 1;
		colChain[n] = 1;
		colBeginDrop[n] = j;

		upColChain = checkUpColColor (i, j, upColChain, 1, up+1);
		downColChain = checkDownColColor (i, j, downColChain, 1, down+1);

		colBeginDrop[n] = j - (downColChain - 1);
		colChain[n] = downColChain + upColChain - 1;
	
		//チェーンデータをもとにdelete
		if (colChain[n] >= 3) {
			//deleteColumnChain (i, colChain, beginDrop, flag);
			return true;
		} else {
			return false;
		}
	}

	public bool countRowChain(int i, int j, int right, int left, int n, bool flag){
		int rightColChain = 1;
		int leftColChain = 1;
		rowChain[n] = 1;
		rowBeginDrop[n] = i;

		rightColChain = checkRightRowColor (i, j, rightColChain, 1, right+1);
		leftColChain = checkLeftRowColor (i, j, leftColChain, 1, left+1);

		rowBeginDrop[n] = i - (leftColChain - 1);
		rowChain[n] = leftColChain + rightColChain - 1;

		//チェーンデータをもとにdelete
		if (rowChain[n] >= 3) {
			//deleteRowChain (c);
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
		n = i;
		m = beginDrop;
		Invoke ("waitFallingDrops", 2.0f);
	}

	public void deleteRowChain(int j, int rowChain, int beginDrop, bool flag){
		for(int i = 0; i < rowChain; i++){
			gc.cube [beginDrop+rowChain-1-i][j].SetActive(false);
			gc.cube [beginDrop+rowChain-1-i].RemoveAt(j);
			//ad.generateAddDrops (beginDrop+rowChain-1-i, flag);
		}
		sc.countScore (rowChain);
	}

	public void waitFallingDrops(){
		dbd.countColumnChain(n,m);
	}
}
