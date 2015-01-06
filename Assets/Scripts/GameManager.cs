using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private GameObject[] allLoot; //array of Loot objects

	void Start(){
		allLoot = GameObject.FindGameObjectsWithTag("Loot");
	}
	
	void FixedUpdate () {
		//Restart the game after a "Tap" if a player is previously dead
		if(PlayerControl.playerIsDead){
			if(Input.GetButtonDown("Jump")){
				PlayerControl.playerIsDead = false;
				Invoke ("RestartLevel", PlayerControl.restartDelay);
			}
		}

		//Destroy remaining loot if the required amount for level is achieved
		if(PlayerControl.currLoot == PlayerControl.levelLoot){
			for(int i=0;i<allLoot.Length;i++){
				Destroy(allLoot[i]);
			}
		}
	}

	void RestartLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}
}
