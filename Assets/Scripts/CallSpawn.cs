using UnityEngine;
using System.Collections;

public class CallSpawn : MonoBehaviour {
	private GameObject spawnerObject;
	private SpawnScript spawnerScript;

	void Awake(){
		GameObject spawnerObject = GameObject.FindWithTag("TileSpawner");
		spawnerScript = spawnerObject.GetComponent<SpawnScript>();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "SpawnTrigger"){
			if(PlayerControl.currScore == PlayerControl.levelScore){
				//If one tile would have more than 2 loot items change allCoint to an array and delete each element
				GameObject allCoins = GameObject.FindWithTag("Loot");
				Destroy(allCoins);
				spawnerScript.SpawnFinal();
			}else{
				spawnerScript.Spawn();
			}		
		}
	}
}
