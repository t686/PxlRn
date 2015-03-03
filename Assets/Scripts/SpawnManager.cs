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
				PlayerControl.maxSpeed = Mathf.Lerp(4, 5, Time.deltaTime);
			}else if(difficulty >= 6 && difficulty < 13){
				spawnCase = Random.Range(0,2);
				PlayerControl.maxSpeed = Mathf.Lerp(5, 6, Time.deltaTime);
			}else if(difficulty >= 13 && difficulty < 21){
				spawnCase = Random.Range(0,3);
				PlayerControl.maxSpeed = Mathf.Lerp(6, 7, Time.deltaTime);
			}else if(difficulty >= 21 && difficulty < 30){
				spawnCase = Random.Range(1,3);
				PlayerControl.maxSpeed = Mathf.Lerp(7, 8, Time.deltaTime);
			}else if(30 <= difficulty){
				spawnCase = 2;
				PlayerControl.maxSpeed = Mathf.Lerp(8, 9, Time.deltaTime);
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