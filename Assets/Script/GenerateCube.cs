using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCube : MonoBehaviour {
	public GameObject blue;
	public GameObject red;
	public GameObject yellow;
	public GameObject green;

	public List<GameObject>[] cube = new List<GameObject>[10];
	public List<GameObject> lineA = new List<GameObject>();
	public List<GameObject> lineB = new List<GameObject>();
	public List<GameObject> lineC = new List<GameObject>();
	public List<GameObject> lineD = new List<GameObject>();
	public List<GameObject> lineE = new List<GameObject>();
	public List<GameObject> lineF = new List<GameObject>();
	public List<GameObject> lineG = new List<GameObject>();
	public List<GameObject> lineH = new List<GameObject>();
	public List<GameObject> lineI = new List<GameObject>();
	public List<GameObject> lineJ = new List<GameObject>();

	AddDrops ad;

	// Use this for initialization
	void Start () {
		cube[0] = lineA;
		cube[1] = lineB;
		cube[2] = lineC;
		cube[3] = lineD;
		cube[4] = lineE;
		cube[5] = lineF;
		cube[6] = lineG;
		cube[7] = lineH;
		cube[8] = lineI;
		cube[9] = lineJ;

		blue.SetActive (true);
	    red.SetActive (true);
		yellow.SetActive (true);
		green.SetActive (true);

		//ゲーム開始時のブロックを生成
		for(int i = 0; i < 10; i++){
			for(int j = 0; j < 10; j++){
				int r = selectColor(i, j);
				strageDrops (r, i, j, -4.5f);
				//cube[i][j] = Instantiate (cube[i][j], new Vector3( 4.5f - i * 1.0f, -4.5f + j * 1.0f, 0),  Quaternion.identity) as GameObject;
			}	
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public int selectColor(int i, int j){
		int r = UnityEngine.Random.Range(0, 4);

		if(j > 1){
			if (r == int.Parse (cube [i] [j - 1].tag)) {
				if (r == int.Parse (cube [i] [j - 2].tag)) {
					r = selectColor (i, j);
				}
			}
		}

		if(i > 1){
			if (r == int.Parse (cube [i-1] [j].tag)) {
				if (r == int.Parse (cube [i-2] [j].tag)) {
					r = selectColor (i, j);
				}
			}
		}
		return r;
	}

	public void strageDrops(int r, int i, int j, float basic){
		switch(r){
			case 0:
			    cube[i].Add(Instantiate (blue, new Vector3( 4.5f - i * 1.0f, basic + j * 1.0f, 0),  Quaternion.identity) as GameObject);
				break;
			case 1:
				cube[i].Add(Instantiate (red, new Vector3( 4.5f - i * 1.0f, basic + j * 1.0f, 0),  Quaternion.identity) as GameObject);
				break;
			case 2:
			    cube[i].Add(Instantiate (yellow, new Vector3( 4.5f - i * 1.0f, basic + j * 1.0f, 0),  Quaternion.identity) as GameObject);
				break;
			case 3:
				cube[i].Add(Instantiate (green, new Vector3( 4.5f - i * 1.0f, basic + j * 1.0f, 0),  Quaternion.identity) as GameObject);
				break;
		}
	}
}
