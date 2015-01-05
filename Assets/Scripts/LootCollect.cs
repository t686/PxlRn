using UnityEngine;
using System.Collections;

public class LootCollect : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			PlayerControl.currLoot += 1;
			PlayerControl.score += PlayerControl.scoreValue;
			audio.enabled = true;
			audio.Play();
			Destroy(this.gameObject);
		}
	}
}
