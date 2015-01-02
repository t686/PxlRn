using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {

	void OnGUI(){
		GUI.Label(new Rect (Screen.width/2, 10, 100, 30), PlayerControl.currScore + " / "+ PlayerControl.levelScore);
	}
}
