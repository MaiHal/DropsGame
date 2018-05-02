using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCube : MonoBehaviour {
	public bool order = false;
	public GameObject[] selectObj = new GameObject[2];

	// Use this for initialization
	void Start () {
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
						changePosition ();
						Debug.Log ("2つめ");
					}
				}
			}
		} else {
			//1つめの選択
			selectObj [0] = clickObj;
			order = true;
			Debug.Log ("1つめ");
		}
	}

	public void changePosition(){
		Vector3 tmp = selectObj[0].transform.position;
		selectObj[0].transform.position = selectObj[1].transform.position;
		selectObj [1].transform.position = tmp;
	}
}
