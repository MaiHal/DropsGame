using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletDrops : MonoBehaviour {
	GenerateCube gc;
	AddDrops ad;
	public List<GameObject> column = new List<GameObject>();

	// Use this for initialization
	void Start () {
		gc = GetComponent<GenerateCube>();
		ad = GetComponent<AddDrops>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//列を消す
	public void deleteColumn(int i){
		int chain = 1;
		for (int j = 1; j < gc.cube [i].Count; j++) {
			if (gc.cube [i] [j].tag.Equals (gc.cube [i] [j - 1].tag) && j != gc.cube[i].Count-1) {
				chain += 1;
			} else if (gc.cube [i] [j].tag.Equals (gc.cube [i] [j - 1].tag) && j == gc.cube[i].Count-1) {
				chain += 1;
				if (chain >= 3) {
					for (int k = 0; k < chain; k++) {
						Destroy (gc.cube [i] [j - k]);
						gc.cube [i].RemoveAt (j - k);
						//ad.generateAddDrops ();
					}
				}
				chain = 1;
			} else {
				if (chain >= 3) {
					for (int k = 1; k < chain + 1; k++) {
						Destroy (gc.cube [i] [j - k]);
						gc.cube [i].RemoveAt (j - k);
						//ad.generateAddDrops ();
					}
				}
				chain = 1;
			}		
		}
	}

	//行を消す
	public void deleteRow(int j){
		Debug.Log (j);
		int chain = 1;
		for (int i = 1; i < 10; i++) {
			if (gc.cube[i].Count > j && gc.cube[i-1].Count > j) {
				if (gc.cube [i] [j].tag.Equals (gc.cube [i - 1] [j].tag) && i != 9) {
					chain += 1;
				} else if (gc.cube [i] [j].tag.Equals (gc.cube [i - 1] [j].tag) && i == 9) {
					chain += 1;
					if (chain >= 3) {
						for (int k = 0; k < chain; k++) {
							Destroy (gc.cube [i - k] [j]);
							gc.cube [i - k].RemoveAt (j);
							//ad.generateAddDrops ();
							Debug.Log ("delete!");
							deleteRow(j);
						}
					}
					chain = 1;
				} else {
					if (chain >= 3) {
						for (int k = 1; k < chain + 1; k++) {
							Destroy (gc.cube [i - k] [j]);
							gc.cube [i - k].RemoveAt (j);
							//ad.generateAddDrops ();
							Debug.Log ("delete!");
							deleteRow(j);
						}
					}
					chain = 1;
				}
			} else {
				if (chain >= 3) {
					for (int k = 1; k < chain + 1; k++) {
						Destroy (gc.cube [i - k] [j]);
						gc.cube [i - k].RemoveAt (j);
						//ad.generateAddDrops ();
						Debug.Log ("delete!");
						deleteRow(j);
					}
				}
				chain = 1;
			}
		}
	}
}
