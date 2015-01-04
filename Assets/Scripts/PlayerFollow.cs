using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour {

	public Transform player;
	public int distance;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.position.x + distance, 0, -10);
	}
}
