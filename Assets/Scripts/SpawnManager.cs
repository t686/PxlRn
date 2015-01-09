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
		if(other.tag == "SpawnTrigger") tileSpawnScript.Spawn();
	}
}