using UnityEngine;
using System.Collections;

public class LootCollect : MonoBehaviour {

	/*void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			if(PlayerControl.currLoot < PlayerControl.levelLoot) PlayerControl.currLoot += 1;
			PlayerControl.score += PlayerControl.scoreValue;
			audio.Play();
			Invoke("DestroyObject",0.1f); //Doing use Invoke, because direstly calling Destroy on a loot Object does not play the audio
		}
	}

	void DestroyObject(){
		Destroy(this.gameObject);
	}*/
}
