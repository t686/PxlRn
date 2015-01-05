using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {
	private GameObject tileSpawnObject;
	private TileSpawn tileSpawnScript;

	void Awake(){
		GameObject tileSpawnObject = GameObject.FindWithTag("TileSpawner");
		tileSpawnScript = tileSpawnObject.GetComponent<TileSpawn>();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "SpawnTrigger"){
			if(PlayerControl.currLoot == PlayerControl.levelLoot){
				//If one tile would have more than 2 loot items change allCoint to an array and delete each element
				GameObject allCoins = GameObject.FindWithTag("Loot");
				Destroy(allCoins);
				tileSpawnScript.SpawnFinal();
			}else{
				tileSpawnScript.Spawn();
			}		
		}
	}
}
