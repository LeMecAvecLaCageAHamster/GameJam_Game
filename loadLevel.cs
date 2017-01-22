using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadLevel : MonoBehaviour {

	// Use this for initialization
	public void OnClick () {
		Application.LoadLevel ("scene1");
		Screen.lockCursor = true;
	}



}
