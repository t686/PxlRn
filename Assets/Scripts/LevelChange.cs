using UnityEngine;
using System.Collections;

public class LevelChange : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			//Reseting the loot and score counters;
			PlayerControl.score = 0;
			PlayerControl.scoreValue *= 1.5;
			PlayerControl.currLoot = 0;
			PlayerControl.levelLoot *= 2;
			audio.Play();
			Invoke("ChangeLevel", 0.5f);
		}

	}

	void ChangeLevel(){
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	
}
