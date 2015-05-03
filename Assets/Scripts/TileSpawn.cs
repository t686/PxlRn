using UnityEngine;
using System.Collections;

public class TileSpawn : MonoBehaviour {

	public GameObject defaultTile;
	public GameObject[] easyTiles;
	public GameObject[] mediumTiles;
	public GameObject[] hardTiles;

	public void SpawnDefault(){
		Instantiate(defaultTile, new Vector3(14,0,0), Quaternion.identity); //Hardcoded position. Sorry) 
	}

	public void SpawnEasyTile(){
		Instantiate(easyTiles[Random.Range(0, easyTiles.Length)], transform.position, Quaternion.identity);
	}

	public void SpawnMediumTile(){
		Instantiate(mediumTiles[Random.Range(0, mediumTiles.Length)], transform.position, Quaternion.identity);
	}

	public void SpawnHardTile(){
		Instantiate(hardTiles[Random.Range(0, hardTiles.Length)], transform.position, Quaternion.identity);
	}
}
