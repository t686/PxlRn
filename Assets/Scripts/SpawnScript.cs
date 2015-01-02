using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	public GameObject[] tiles;
	public GameObject finalTile;

	//Instantiate a random Tile from obj array[];
	public void Spawn(){
		Instantiate(tiles[Random.Range(0, tiles.Length)], transform.position, Quaternion.identity);
	}
	
	//Instantiate the final Tile
	public void SpawnFinal(){
		Instantiate(finalTile, transform.position, Quaternion.identity);
	}
}
