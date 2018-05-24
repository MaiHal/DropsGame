using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleToPlay : MonoBehaviour {

	public void moveToPlay(){
		SceneManager.LoadScene ("Main");
	}
}
