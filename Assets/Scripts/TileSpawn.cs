using UnityEngine;
using System.Collections;

public class TileSpawn : MonoBehaviour {

	public GameObject[] tiles;
	
	//Instantiate a random Tile from array[] tiles;
	public void Spawn(){
		Instantiate(tiles[Random.Range(0, tiles.Length)], transform.position, Quaternion.identity);
	}
}
