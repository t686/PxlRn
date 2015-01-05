using UnityEngine;
using System.Collections;

public class LevelChange : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			Application.LoadLevel(Application.loadedLevel + 1);
			//Reseting the loot and score counters;
			PlayerControl.score = 0;
			PlayerControl.scoreValue *= 1.5;
			PlayerControl.currLoot = 0;
			PlayerControl.levelLoot *= 2;
			//Play music here
		}
	}
	
}
