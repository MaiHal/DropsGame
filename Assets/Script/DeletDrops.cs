using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletDrops : MonoBehaviour {
	GenerateCube gc;
	public List<GameObject> column = new List<GameObject>();

	// Use this for initialization
	void Start () {
		gc = GetComponent<GenerateCube>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//列を消す
	public void deleteColumn(int i){
		int chain = 1;
		for (int j = 1; j < gc.cube [i].Count; j++) {
			Debug.Log("縦見てるよ"+i+"列目"+j+"番目");
			if (gc.cube [i] [j].tag.Equals (gc.cube [i] [j - 1].tag) && j != gc.cube[i].Count-1) {
				chain += 1;
			} else if (gc.cube [i] [j].tag.Equals (gc.cube [i] [j - 1].tag) && j == gc.cube[i].Count-1) {
				chain += 1;
				if (chain >= 3) {
					for (int k = 0; k < chain; k++) {
						Debug.Log ("縦");
						Destroy (gc.cube [i] [j - k]);
						gc.cube [i].RemoveAt (j - k);
					}
				}
				chain = 1;
			} else {
				if (chain >= 3) {
					for (int k = 1; k < chain + 1; k++) {
						Debug.Log ("縦");
						Destroy (gc.cube [i] [j - k]);
						gc.cube [i].RemoveAt (j - k);
					}
				}
				chain = 1;
			}		
		}
	}

	//行を消す
	public void deleteRow(int j){
		int chain = 1;
		for (int i = 1; i < 10; i++) {
			Debug.Log ("横見てるよ"+j+"列目"+i+"番目");
			if (gc.cube[i].Count > j && gc.cube[i-1].Count > j) {
				if (gc.cube [i] [j].tag.Equals (gc.cube [i - 1] [j].tag) && i != 9) {
					Debug.Log (j);
					chain += 1;
				} else if (gc.cube [i] [j].tag.Equals (gc.cube [i - 1] [j].tag) && i == 9) {
					chain += 1;
					if (chain >= 3) {
						for (int k = 0; k < chain; k++) {
							Debug.Log ("横" + (i - k));
							Destroy (gc.cube [i - k] [j]);
							gc.cube [i - k].RemoveAt (j);
						}
					}
					chain = 1;
				} else {
					if (chain >= 3) {
						for (int k = 1; k < chain + 1; k++) {
							Debug.Log ("横" + (i - k));
							Destroy (gc.cube [i - k] [j]);
							gc.cube [i - k].RemoveAt (j);
						}
					}
					chain = 1;
				}
			} else {
				Debug.Log ("a"+gc.cube[i].Count+">"+j);
				if (chain >= 3) {
					for (int k = 1; k < chain + 1; k++) {
						Debug.Log ("横" + (i - k));
						Destroy (gc.cube [i - k] [j]);
						gc.cube [i - k].RemoveAt (j);
					}
				}
				chain = 1;
			}
		}
	}
}
