using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCube : MonoBehaviour {
	public bool order = false;
	public GameObject[] selectObj = new GameObject[2];
	GenerateCube gc;
	bool flag = false;
	int i;
	int j;
	int k;
	int l;

	// Use this for initialization
	void Start () {
		gc = GetComponent<GenerateCube>();
	}
	
	// Update is called once per frame
	void Update () {
		getClickObject ();
	}

	public void getClickObject() { 
		// 左クリックされた場所のオブジェクトを取得
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit)) {
				selectObject (hit.collider.gameObject);
			}
		}
	}


	public void selectObject(GameObject clickObj){
		if (order) {
			//2つめの選択(1つめのドロップの前後左右かどうか)
			if ((clickObj.transform.position.x != selectObj[0].transform.position.x) ||
			    (clickObj.transform.position.y != selectObj[0].transform.position.y)) {
				if (((clickObj.transform.position.x == selectObj [0].transform.position.x) &&
				    (Mathf.Abs (clickObj.transform.position.y - selectObj [0].transform.position.y)) < 1.1f) ||
					((Mathf.Abs (clickObj.transform.position.y - selectObj [0].transform.position.y) < 0.2f) &&
				    (Mathf.Abs (clickObj.transform.position.x - selectObj [0].transform.position.x) < 1.1f))) {
					//1つめと同じ色でないか
					if (clickObj.tag != selectObj [0].tag) {
						selectObj [1] = clickObj;
						order = false;
						changePosition (selectObj);
					}
				}
			}
		} else {
			//1つめの選択
			selectObj [0] = clickObj;
			order = true;
		}
	}

	public void changePosition(GameObject[] obj){
		//クリックしたオブジェクトの添字を計算
		i = (int)(4.5f - obj[0].transform.position.x);
		j = (int)Mathf.Round(4.5f + obj [0].transform.position.y);
		k = (int)(4.5f - obj[1].transform.position.x);
		l = (int)Mathf.Round(4.5f + obj [1].transform.position.y);

		//GameObject(中身)の座標を交換
		Vector3 tmpPos = selectObj[0].transform.position;
		obj[0].transform.position = selectObj[1].transform.position;
		obj[1].transform.position = tmpPos;

		//リストの中身を交換
		GameObject tmpDrop = gc.cube[i][j];
		gc.cube [i] [j] = gc.cube [k] [l];
		gc.cube [k] [l] = tmpDrop;

		flag = false;
		Invoke("waitChangeDrops", 0.2f);
	}

	public void waitChangeDrops(){
		//交換によって削除する
		flag = this.gameObject.GetComponent<DeleteByChange> ().countColumnChain (i, j, flag);
		if (i == k && flag == true && l > j) {
			l = l - 3;
			Invoke ("waitAddDrops", 0.2f);
		} else {
			Invoke ("waitAddDrops", 0.2f);
		}
	
	}

	public void waitAddDrops(){
		this.gameObject.GetComponent<DeleteByChange> ().countColumnChain (k, l, flag);
	}
}
