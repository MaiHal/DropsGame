using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteManegement : MonoBehaviour {
	bool[] columnFlag = new bool[2];
	bool[] rowFlag = new bool[2];
	bool deleteFlag = false;
	public int i,j,k,l;
	//DeleteByChange dbc;

	// Use this for initialization
	void Start () {
		//dbc = GetComponent<DeleteByChange>();
		columnFlag[0] = false;
		columnFlag [1] = false;
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
			
			if (j > l) {
				//行チェック
				rowFlag [0] = this.gameObject.GetComponent<DeleteByChange> ().countRowChain (i, j, 2, 2, false);
				rowFlag [1] = this.gameObject.GetComponent<DeleteByChange> ().countRowChain (k, l, 2, 2, rowFlag [0]);

				//列チェック　上半分と下半分
				columnFlag [0] = this.gameObject.GetComponent<DeleteByChange> ().countColumnChain (i, j, 2, 0, false);
				columnFlag [1] = this.gameObject.GetComponent<DeleteByChange> ().countColumnChain (k, l, 0, 2, columnFlag [0]);
			} else {
				rowFlag [0] = this.gameObject.GetComponent<DeleteByChange> ().countRowChain (k, l, 2, 2, false);
				rowFlag [1] = this.gameObject.GetComponent<DeleteByChange> ().countRowChain (i, j, 2, 2, rowFlag [0]);

				columnFlag [0] = this.gameObject.GetComponent<DeleteByChange> ().countColumnChain (k, l, 2, 0, false);
				columnFlag [1] = this.gameObject.GetComponent<DeleteByChange> ().countColumnChain (i, j, 0, 2, columnFlag [0]);
			}
		} else {
			//列チェック
			columnFlag [0] = this.gameObject.GetComponent<DeleteByChange> ().countColumnChain (i, j, 2, 2, false);
			columnFlag [1] = this.gameObject.GetComponent<DeleteByChange> ().countColumnChain (k, l, 2, 2, columnFlag [0]);

			if (k > i) {
				columnFlag [0] = this.gameObject.GetComponent<DeleteByChange> ().countColumnChain (k, l, 2, 2, false);
				columnFlag [1] = this.gameObject.GetComponent<DeleteByChange> ().countColumnChain (i, j, 2, 2, columnFlag [0]);

				//行チェック 右半分と左半分
				rowFlag [0] = this.gameObject.GetComponent<DeleteByChange> ().countRowChain (k, l, 2, 0, false);
				rowFlag [1] = this.gameObject.GetComponent<DeleteByChange> ().countRowChain (i, j, 0, 2, columnFlag [0]);
			} else {
				columnFlag [0] = this.gameObject.GetComponent<DeleteByChange> ().countColumnChain (i, j, 2, 2, false);
				columnFlag [1] = this.gameObject.GetComponent<DeleteByChange> ().countColumnChain (k, l, 2, 2, columnFlag [0]);

				rowFlag [0] = this.gameObject.GetComponent<DeleteByChange> ().countRowChain (i, j, 2, 0, false);
				rowFlag [1] = this.gameObject.GetComponent<DeleteByChange> ().countRowChain (k, l, 0, 2, columnFlag [0]);
			}
		}

		if (columnFlag [0] == false && columnFlag [1] == false && rowFlag [0] == false && rowFlag [1] == false) {
			deleteFlag = false;
		} else {
			deleteFlag = true;
		}
		Debug.Log ("行"+rowFlag[0]+","+rowFlag[1]+" :列"+columnFlag[0]+","+columnFlag[1]);
	}
}
