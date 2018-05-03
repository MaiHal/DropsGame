using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletDrops : MonoBehaviour {
	GenerateCube gc;
	public List<GameObject> column = new List<GameObject>();
	public int chain;

	// Use this for initialization
	void Start () {
		gc = GetComponent<GenerateCube>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//列を消す
	public void deleteColumn(int i){
		chain = 1;
		for (int j = 1; j < gc.cube [i].Count; j++) {
			if (gc.cube [i] [j].tag.Equals (gc.cube [i] [j - 1].tag) && j != gc.cube[i].Count-1) {
				chain += 1;
			} else if (gc.cube [i] [j].tag.Equals (gc.cube [i] [j - 1].tag) && j == gc.cube[i].Count-1) {
				chain += 1;
				if (chain >= 3) {
					for (int k = 0; k < chain; k++) {
						Destroy (gc.cube [i] [j - k]);
						gc.cube [i].RemoveAt (j - k);
					}
				}
				chain = 1;
			} else {
				if (chain >= 3) {
					for (int k = 1; k < chain + 1; k++) {
						Destroy (gc.cube [i] [j - k]);
						gc.cube [i].RemoveAt (j - k);
					}
				}
				chain = 1;
			}		
		}
	}

	//行を消す
	public void deleteRow(){
		
	}
}
