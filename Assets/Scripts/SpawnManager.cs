using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {
	private GameObject tileSpawnObject;
	private TileSpawn tileSpawnScript;
	private int difficulty;
	private int randRoll;
	private int spawnCase;

	void Awake(){
		GameObject tileSpawnObject = GameObject.FindWithTag("TileSpawner");
		tileSpawnScript = tileSpawnObject.GetComponent<TileSpawn>();
	}


	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "SpawnTrigger"){
			difficulty = PlayerControl.tilesPassed;

			//Determine which level of difficult should the next tile be
			if(difficulty < 6){
				spawnCase = 0;
			}else if(difficulty >= 6 && difficulty < 13){
				spawnCase = Random.Range(0,2);
			}else if(difficulty >= 13 && difficulty < 21){
				spawnCase = Random.Range(0,3);
			}else if(difficulty >= 21 && difficulty < 30){
				spawnCase = Random.Range(1,3);
			}else if(30 <= difficulty){
				spawnCase = 2;
			}

			switch(spawnCase){
			case 1:
				tileSpawnScript.SpawnMediumTile();
				break;
			case 2:
				tileSpawnScript.SpawnHardTile();
				break;
			default:
				tileSpawnScript.SpawnEasyTile();
				break;
			}

		}else if(other.tag == "Player"){
			PlayerControl.tilesPassed ++;
		}

	}
}