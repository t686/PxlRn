using UnityEngine;
using System.Collections;

public class ChangeLevel : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			Application.LoadLevel(Application.loadedLevel + 1);
			PlayerControl.currScore = 0;
			PlayerControl.levelScore *= 2;
		}
	}
	
}
