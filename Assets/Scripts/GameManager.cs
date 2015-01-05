using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
		if(PlayerControl.playerIsDead){
			if(Input.GetButtonDown("Jump")){
				PlayerControl.playerIsDead = false;
				Invoke ("RestartLevel", PlayerControl.restartDelay);
			}
		}
	}

	void RestartLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}
}
